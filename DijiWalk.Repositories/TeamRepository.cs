//-----------------------------------------------------------------------
// <copyright file="TeamRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Business.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Team to the database
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly SmartCityContext _context;

        private readonly ITeamBusiness _teamBusiness;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamRepository(SmartCityContext context, ITeamBusiness teamBusiness)
        {
            _context = context;
            _teamBusiness = teamBusiness;
        }

        /// <summary>
        /// Method to Add the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Add</param>
        public void Add(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Team passed in the parameters
        /// </summary>
        /// <param name="idTeam">Object Team to Delete</param>
        public async Task<ApiResponse> Delete(int idTeam)
        {
            try
            {
                if (!await _teamBusiness.ContainsTeams(idTeam))
                {
                    await _teamBusiness.DeleteAllFromTeam(idTeam);
                }
                _context.Teams.Remove(await _context.Teams.FindAsync(idTeam));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        

        /// <summary>
        /// Method to find an Team with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Team</param>
        /// <returns>The Team with the Id researched</returns>
        public async Task<Team> Find(int id)
        {
            return await _context.Teams.Where(t => t.Id == id).Include(t => t.TeamPlayers).ThenInclude(tp => tp.Player).Include(t => t.TeamRoutes).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to find all Team from the database
        /// </summary>
        /// <returns>A List with all Teams</returns>
        public async Task<IEnumerable<Team>> FindAll()
        {
            return await _context.Teams.Include(t => t.TeamPlayers).ThenInclude(tp => tp.Player).Include(t => t.TeamRoutes).ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Update</param>
        public void Update(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }
    }
}
