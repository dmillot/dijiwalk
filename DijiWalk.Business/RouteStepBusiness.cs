using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class RouteStepBusiness : IRouteStepBusiness
    {

        private readonly SmartCityContext _context;


        public RouteStepBusiness(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Delete all route step 
        /// </summary>
        /// <param name="routeSteps">List of route steps to delete</param>
        public async Task<ApiResponse> DeleteFromRoute(List<RouteStep> routeSteps)
        {
            try
            {
                _context.Routesteps.RemoveRange(routeSteps);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to Add a list of routestep passed in the parameters to the database
        /// </summary>
        /// <param name="routeSteps">List of object RouteStep to Add</param>
        public async Task<ApiResponse> AddRange(List<RouteStep> routeSteps)
        {
            try
            {
                await _context.Routesteps.AddRangeAsync(routeSteps);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = routeSteps };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }


        /// <summary>
        /// Method to set up list of route step to add to a route
        /// </summary>
        /// <param name="routeStep">Object list of route step to set up</param>
        /// <param name="idRoute">Id of the route</param>
        public async Task<ApiResponse> SetUp(List<RouteStep> routeStep, int idRoute)
        {
            return await this.AddRange(routeStep.Select(r =>
            {
                r.IdRoute = idRoute;
                return r;
            }).ToList());
        }
    }
}
