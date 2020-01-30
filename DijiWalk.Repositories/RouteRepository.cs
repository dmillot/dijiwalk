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
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Add</param>
        public void Add(Route route)
        {
            this._smartCityContext.Routes.Add(route);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Route passed in the parameters
        /// </summary>
        /// <param name="route">Object Route to Delete</param>
        public void Delete(Route route)
        {
            this._smartCityContext.Routes.Remove(route);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Route with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Route</param>
        /// <returns>The Route with the Id researched</returns>
        public Route Find(int id)
        {
            return this._smartCityContext.Routes.Find(id);
        }

        /// <summary>
        /// Method to find all Route from the database
        /// </summary>
        /// <returns>A List with all Routes</returns>
        public IEnumerable<Route> FindAll()
        {
            return this._smartCityContext.Routes;
        }

        /// <summary>
        /// Method that will Update the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Update</param>
        public void Update(Route route)
        {
            this._smartCityContext.Routes.Update(route);
            this._smartCityContext.SaveChanges();
        }
    }
}
