using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IRouteStepBusiness
    {

        /// <summary>
        /// Method to Delete all route step 
        /// </summary>
        /// <param name="routeSteps">List of route steps to delete</param>
        Task<ApiResponse> DeleteFromRoute(List<RouteStep> routeSteps);

        /// <summary>
        /// Method to Add a list of routestep passed in the parameters to the database
        /// </summary>
        /// <param name="routeSteps">List of object RouteStep to Add</param>
        Task<ApiResponse> AddRange(List<RouteStep> routeSteps);



        /// <summary>
        /// Method to set up list of route step to add to a route
        /// </summary>
        /// <param name="routeStep">Object list of route step to set up</param>
        /// <param name="idRoute">Id of the route</param>
        Task<ApiResponse> SetUp(List<RouteStep> routeStep, int idRoute);

    }
}
