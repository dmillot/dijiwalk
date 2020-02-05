//-----------------------------------------------------------------------
// <copyright file="TeamRouteRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        public void Delete(TeamRoute teamRoute)
        {
           _context.Teamroutes.Remove(teamRoute);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an TeamRoute with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the TeamRoute</param>
        /// <returns>The TeamRoute with the Id researched</returns>
        public async Task<TeamRoute> Find(int id)
        {
            return await _context.Teamroutes.FindAsync(id);
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
        public void Update(TeamRoute teamRoute)
        {
           _context.Teamroutes.Update(teamRoute);
           _context.SaveChanges();
        }
    }
}
