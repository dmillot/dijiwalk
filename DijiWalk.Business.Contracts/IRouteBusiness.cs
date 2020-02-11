using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IRouteBusiness
    {
        /// <summary>
        /// Method to Delete all route step and tag of a route
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        Task<ApiResponse> DeleteAllFromRoute(int idRoute);


        /// <summary>
        /// Method to separate step and route 
        /// </summary>
        /// <param name="route">Object route</param>
        Route SeparateStep(Route route);

    }
}
