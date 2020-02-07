using DijiWalk.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IRouteBusiness
    {
        /// <summary>
        /// Method to Delete all route step of a route
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        Task<ApiResponse> DeleteAllFromRoute(int idRoute);

    }
}
