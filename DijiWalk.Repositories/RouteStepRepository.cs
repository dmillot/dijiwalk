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
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the RouteStep passed in the parameters to the database
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Add</param>
        public void Add(RouteStep routeStep)
        {
            this._smartCityContext.Routesteps.Add(routeStep);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the RouteStep passed in the parameters
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Delete</param>
        public void Delete(RouteStep routeStep)
        {
            this._smartCityContext.Routesteps.Remove(routeStep);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an RouteStep with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the RouteStep</param>
        /// <returns>The RouteStep with the Id researched</returns>
        public RouteStep Find(int id)
        {
            return this._smartCityContext.Routesteps.Find(id);
        }

        /// <summary>
        /// Method to find all RouteStep from the database
        /// </summary>
        /// <returns>A List with all RouteSteps</returns>
        public IEnumerable<RouteStep> FindAll()
        {
            return this._smartCityContext.Routesteps;
        }

        /// <summary>
        /// Method that will Update the RouteStep passed in the parameters to the database
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Update</param>
        public void Update(RouteStep routeStep)
        {
            this._smartCityContext.Routesteps.Update(routeStep);
            this._smartCityContext.SaveChanges();
        }
    }
}
