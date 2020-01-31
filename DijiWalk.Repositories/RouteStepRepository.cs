//-----------------------------------------------------------------------
// <copyright file="RouteStepRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;

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
        public RouteStep Find(int id)
        {
            return _context.Routesteps.Find(id);
        }

        /// <summary>
        /// Method to find all RouteStep from the database
        /// </summary>
        /// <returns>A List with all RouteSteps</returns>
        public IEnumerable<RouteStep> FindAll()
        {
            return _context.Routesteps;
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
