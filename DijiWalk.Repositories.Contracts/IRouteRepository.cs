//-----------------------------------------------------------------------
// <copyright file="IRouteRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Route repository
    /// </summary>
    public interface IRouteRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Add</param>
        void Add(Route route);

        /// <summary>
        /// Definition of the function that will Delete from the database the Route passed in the parameters
        /// </summary>
        /// <param name="route">Object Route to Delete</param>
        Task<ApiResponse> Delete(int idRoute);

        /// <summary>
        /// Definition of the method to find an Route with his Id
        /// </summary>
        /// <param name="id">The Id of the Route</param>
        /// <returns>The Route with the Id researched</returns>
        Task<Route> Find(int id);

        /// <summary>
        /// Definition of the method to find all Route
        /// </summary>
        /// <returns>A List of Routes</returns>
        Task<IEnumerable<Route>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Update</param>
        void Update(Route route);
    }
}
