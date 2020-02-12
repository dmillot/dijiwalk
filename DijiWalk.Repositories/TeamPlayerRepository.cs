//-----------------------------------------------------------------------
// <copyright file="TeamPlayerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object TeamPlayer to the database
    /// </summary>
    public class TeamPlayerRepository : ITeamPlayerRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamPlayerRepository(SmartCityContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to Add the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Add</param>
        public void Add(TeamPlayer teamPlayer)
        {
           _context.Teamplayers.Add(teamPlayer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamPlayer passed in the parameters
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Delete</param>
        public async Task<ApiResponse> Delete(int idTeamPlayer)
        {
            try
            {
                _context.Teamplayers.Remove(await _context.Teamplayers.FindAsync(idTeamPlayer));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an TeamPlayer with team id in the database
        /// </summary>
        /// <param name="id">The Id of the team</param>
        /// <returns>The TeamPlayer with the Id team researched</returns>
        public async Task<IEnumerable<TeamPlayer>> FindByTeam(int id)
        {
            return await _context.Teamplayers
                .Where(t => t.IdTeam == id)
                .Include(p => p.Player)
                .ToListAsync();
        }

        /// <summary>
        /// Method to find all TeamPlayer from the database
        /// </summary>
        /// <returns>A List with all TeamPlayers</returns>
        public async Task<IEnumerable<TeamPlayer>> FindAll()
        {
            return await _context.Teamplayers
                .Include(p => p.Player)
                .ToListAsync();
        }

        /// <summary>
        /// Method that will Update the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Update</param>
        public void Update(TeamPlayer teamPlayer)
        {
           _context.Teamplayers.Update(teamPlayer);
           _context.SaveChanges();
        }
    }
}
