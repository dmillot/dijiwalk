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
    public class RouteTagBusiness : IRouteTagBusiness
    {

        private readonly SmartCityContext _context;


        public RouteTagBusiness(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add a list of RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTags">List of object RouteTag to Add</param>
        public async Task<ApiResponse> AddRange(List<RouteTag> routeTags)
        {
            try
            {
                await _context.Routetags.AddRangeAsync(routeTags);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = routeTags };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }


        /// <summary>
        /// Method to set up list of route rag to add to a route
        /// </summary>
        /// <param name="routeTag">Object list of route tag to set up</param>
        /// <param name="idRoute">Id of the route</param>
        public async Task<ApiResponse> SetUp(List<RouteTag> routeTags, int idRoute)
        {
            return await this.AddRange(routeTags.Select(r =>
            {
                r.IdRoute = idRoute;
                return r;
            }).ToList());
        }


        /// <summary>
        /// Method to Delete all route tag 
        /// </summary>
        /// <param name="routeTags">List of route tags to delete</param>
        public async Task<ApiResponse> DeleteFromRoute(List<RouteTag> routeTags)
        {
            try
            {
                _context.Routetags.RemoveRange(routeTags);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }
    }
}
