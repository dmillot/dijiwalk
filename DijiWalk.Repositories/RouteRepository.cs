//-----------------------------------------------------------------------
// <copyright file="RouteRepository.cs" company="DijiWalk">
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
    public class RouteRepository : IRouteRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public RouteRepository(SmartCityContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to Add the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Add</param>
        public void Add(Route route)
        {
           _context.Routes.Add(route);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Route passed in the parameters
        /// </summary>
        /// <param name="route">Object Route to Delete</param>
        public void Delete(Route route)
        {
           _context.Routes.Remove(route);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Route with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Route</param>
        /// <returns>The Route with the Id researched</returns>
        public Route Find(int id)
        {
            return _context.Routes.Find(id);
        }

        /// <summary>
        /// Method to find all Route from the database
        /// </summary>
        /// <returns>A List with all Routes</returns>
        public IEnumerable<Route> FindAll()
        {
            return _context.Routes;
        }

        /// <summary>
        /// Method that will Update the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Update</param>
        public void Update(Route route)
        {
           _context.Routes.Update(route);
           _context.SaveChanges();
        }
    }
}
