//-----------------------------------------------------------------------
// <copyright file="RouteTagRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object RouteTag to the database
    /// </summary>
    public class RouteTagRepository : IRouteTagRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public RouteTagRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Add</param>
        public void Add(RouteTag routeTag)
        {
            _context.Routetags.Add(routeTag);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the RouteTag passed in the parameters
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Delete</param>
        public void Delete(RouteTag routeTag)
        {
            _context.Routetags.Remove(routeTag);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an RouteTag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the RouteTag</param>
        /// <returns>The RouteTag with the Id researched</returns>
        public RouteTag Find(int id)
        {
            return _context.Routetags.Find(id);
        }

        /// <summary>
        /// Method to find all RouteTag from the database
        /// </summary>
        /// <returns>A List with all RouteTags</returns>
        public IEnumerable<RouteTag> FindAll()
        {
            return _context.Routetags;
        }

        /// <summary>
        /// Method that will Update the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Update</param>
        public void Update(RouteTag routeTag)
        {
            _context.Routetags.Update(routeTag);
            _context.SaveChanges();
        }
    }
}
