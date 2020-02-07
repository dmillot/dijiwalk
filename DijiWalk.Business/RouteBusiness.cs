using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
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


        public RouteBusiness(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Delete all route step and tag of a route
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        public async Task<ApiResponse> DeleteAllFromRoute(int idRoute)
        {
            try
            {
                _context.Routesteps.RemoveRange(await _context.Routesteps.Where(x => x.IdRoute == idRoute).ToListAsync());
                _context.Routetags.RemoveRange(await _context.Routetags.Where(x => x.IdRoute == idRoute).ToListAsync());
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }
    }
}
