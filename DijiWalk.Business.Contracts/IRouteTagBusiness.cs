using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IRouteTagBusiness
    {

        /// <summary>
        /// Method to Delete all route tag 
        /// </summary>
        /// <param name="routeTags">List of route tags to delete</param>
        Task<ApiResponse> DeleteFromRoute(List<RouteTag> routeTags);

        /// <summary>
        /// Method to Add a list of RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTags">List of object RouteTag to Add</param>
        Task<ApiResponse> AddRange(List<RouteTag> routeTags);


        /// <summary>
        /// Method to set up list of route rag to add to a route
        /// </summary>
        /// <param name="routeTag">Object list of route tag to set up</param>
        /// <param name="idRoute">Id of the route</param>
        Task<ApiResponse> SetUp(List<RouteTag> routeTags, int idRoute);

    }
}
