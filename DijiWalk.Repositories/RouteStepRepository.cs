//-----------------------------------------------------------------------
// <copyright file="RouteStepRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Route to the database
    /// </summary>
    public class RouteStepRepository : IRouteStepRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public RouteStepRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the RouteStep passed in the parameters to the database
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Add</param>
        public void Add(RouteStep routeStep)
        {
            _context.Routesteps.Add(routeStep);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the RouteStep passed in the parameters
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Delete</param>
        public void Delete(RouteStep routeStep)
        {
            _context.Routesteps.Remove(routeStep);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an RouteStep with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the RouteStep</param>
        /// <returns>The RouteStep with the Id researched</returns>
        public async Task<RouteStep> Find(int id)
        {
            return await _context.Routesteps.FindAsync(id);
        }

        /// <summary>
        /// Method to find all RouteStep from the database
        /// </summary>
        /// <returns>A List with all RouteSteps</returns>
        public async Task<IEnumerable<RouteStep>> FindAll()
        {
            return await _context.Routesteps.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the RouteStep passed in the parameters to the database
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Update</param>
        public void Update(RouteStep routeStep)
        {
            _context.Routesteps.Update(routeStep);
            _context.SaveChanges();
        }
    }
}
