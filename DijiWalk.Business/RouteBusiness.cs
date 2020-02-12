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
    public class RouteBusiness : IRouteBusiness
    {

        private readonly SmartCityContext _context;

        private readonly IRouteStepBusiness _routeStepBusiness;

        private readonly IRouteTagBusiness _routeTagBusiness;


        public RouteBusiness(SmartCityContext context, IRouteStepBusiness routeStepBusiness, IRouteTagBusiness routeTagBusiness)
        {
            _context = context;
            _routeStepBusiness = routeStepBusiness;
            _routeTagBusiness = routeTagBusiness;
        }


        /// <summary>
        /// Method to Delete all route step and tag of a route
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        public async Task<ApiResponse> DeleteAllFromRoute(int idRoute)
        {
            try
            {
                await _routeStepBusiness.DeleteFromRoute(await _context.Routesteps.Where(x => x.IdRoute == idRoute).ToListAsync());
                await _routeTagBusiness.DeleteFromRoute(await _context.Routetags.Where(x => x.IdRoute == idRoute).ToListAsync());
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to separate step and route 
        /// </summary>
        /// <param name="route">Object route</param>
        public Route SeparateStep(Route route)
        {
            return new Route(route);
        }
    }
}
