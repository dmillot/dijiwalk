//-----------------------------------------------------------------------
// <copyright file="IRouteStepRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the RouteStep repository
    /// </summary>
    public interface IRouteStepRepository
    {
        /// <summary>
        /// Definition of the function that will Add the RouteStep passed in the parameters to the database
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Add</param>
        void Add(RouteStep routeStep);

        /// <summary>
        /// Definition of the function that will Delete from the database the RouteStep passed in the parameters
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Delete</param>
        void Delete(RouteStep routeStep);

        /// <summary>
        /// Definition of the method to find an RouteStep with his Id
        /// </summary>
        /// <param name="id">The Id of the RouteStep</param>
        /// <returns>The RouteStep with the Id researched</returns>
        RouteStep Find(int id);

        /// <summary>
        /// Definition of the method to find all RouteStep
        /// </summary>
        /// <returns>A List of RouteSteps</returns>
        IEnumerable<RouteStep> FindAll();

        /// <summary>
        /// Definition of the function that will Update the RouteStep passed in the parameters to the database
        /// </summary>
        /// <param name="routeStep">Object RouteStep to Update</param>
        void Update(RouteStep routeStep);
    }
}
