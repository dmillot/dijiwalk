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
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Add</param>
        public void Add(RouteTag routeTag)
        {
            this._smartCityContext.Routetags.Add(routeTag);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the RouteTag passed in the parameters
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Delete</param>
        public void Delete(RouteTag routeTag)
        {
            this._smartCityContext.Routetags.Remove(routeTag);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an RouteTag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the RouteTag</param>
        /// <returns>The RouteTag with the Id researched</returns>
        public RouteTag Find(int id)
        {
            return this._smartCityContext.Routetags.Find(id);
        }

        /// <summary>
        /// Method to find all RouteTag from the database
        /// </summary>
        /// <returns>A List with all RouteTags</returns>
        public IEnumerable<RouteTag> FindAll()
        {
            return this._smartCityContext.Routetags;
        }

        /// <summary>
        /// Method that will Update the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Update</param>
        public void Update(RouteTag routeTag)
        {
            this._smartCityContext.Routetags.Update(routeTag);
            this._smartCityContext.SaveChanges();
        }
    }
}
