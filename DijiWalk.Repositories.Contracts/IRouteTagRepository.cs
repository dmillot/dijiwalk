//-----------------------------------------------------------------------
// <copyright file="IRouteTagRepository.cs" company="DijiWalk">
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
    /// This is the interface for the RouteStep repository
    /// </summary>
    public interface IRouteTagRepository
    {
        /// <summary>
        /// Definition of the function that will Add the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Add</param>
        void Add(RouteTag routeTag);

        /// <summary>
        /// Definition of the function that will Delete from the database the RouteTag passed in the parameters
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Delete</param>
        Task<ApiResponse> Delete(int idRouteTag);


        /// <summary>
        /// Method to Delete all route tag of a route
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        Task<ApiResponse> DeleteAll(int idRoute);

        /// <summary>
        /// Definition of the method to find an RouteTag with his Id
        /// </summary>
        /// <param name="id">The Id of the RouteTag</param>
        /// <returns>The RouteTag with the Id researched</returns>
        Task<RouteTag> Find(int id);

        /// <summary>
        /// Definition of the method to find all RouteTag
        /// </summary>
        /// <returns>A List of RouteTags</returns>
        Task<IEnumerable<RouteTag>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Update</param>
        void Update(RouteTag routeTag);
    }
}
