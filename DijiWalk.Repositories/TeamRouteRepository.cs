//-----------------------------------------------------------------------
// <copyright file="TeamRouteRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object TeamRoute to the database
    /// </summary>
    public class TeamRouteRepository : ITeamRouteRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamRouteRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the TeamRoute passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Add</param>
        public void Add(TeamRoute teamRoute)
        {
            _context.Teamroutes.Add(teamRoute);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamRoute passed in the parameters
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Delete</param>
        public async Task<ApiResponse> Delete(int idTeamRoute)
        {
            try
            {
                _context.Teamroutes.Remove(await _context.Teamroutes.FindAsync(idTeamRoute));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an TeamRoute with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the game</param>
        /// <returns>The TeamRoute with the Id researched</returns>
        public async Task<List<TeamRoute>> FindInGame(int id)
        {
            return await _context.Teamroutes.Where(t => t.IdGame == id).Include(t => t.Game).Include(t => t.Team).ThenInclude(t => t.Captain).Include(t => t.RouteStep).ThenInclude(rs => rs.Route).Include(t => t.RouteStep).ThenInclude(rs => rs.Step).ToListAsync();
        }

        /// <summary>
        /// Method to find all TeamRoute from the database
        /// </summary>
        /// <returns>A List with all TeamRoutes</returns>
        public async Task<IEnumerable<TeamRoute>> FindAll()
        {
            return await _context.Teamroutes.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the TeamRoute passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Update</param>
        public async Task<ApiResponse> Update(TeamRoute teamRoute, bool validate)
        {
            try
            {
                var oldteamRoute = await _context.Teamroutes.FirstOrDefaultAsync(rt => rt.Id == teamRoute.Id);
                oldteamRoute.ValidationDate = teamRoute.ValidationDate;
                oldteamRoute.Validate = teamRoute.Validate;
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = (validate) ? "Validation effectuée avec succées !" : "Reject effectué avec succées !", Response = teamRoute };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }
    }
}
