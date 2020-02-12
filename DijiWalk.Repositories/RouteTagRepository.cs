//-----------------------------------------------------------------------
// <copyright file="RouteTagRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using DijiWalk.Common.Response;

    /// <summary>
    /// Class that connect the Object RouteTag to the database
    /// </summary>
    public class RouteTagRepository : IRouteTagRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public RouteTagRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Add</param>
        public void Add(RouteTag routeTag)
        {
            _context.Routetags.Add(routeTag);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the RouteTag passed in the parameters
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Delete</param>
        public async Task<ApiResponse> Delete(int idRouteTag)
        {
            try
            {
                _context.Routetags.Remove(await _context.Routetags.FindAsync(idRouteTag));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to find an RouteTag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the RouteTag</param>
        /// <returns>The RouteTag with the Id researched</returns>
        public async Task<RouteTag> Find(int id)
        {
            return await _context.Routetags.FindAsync(id);
        }

        /// <summary>
        /// Method to find all RouteTag from the database
        /// </summary>
        /// <returns>A List with all RouteTags</returns>
        public async Task<IEnumerable<RouteTag>> FindAll()
        {
            return await _context.Routetags.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the RouteTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object RouteTag to Update</param>
        public void Update(RouteTag routeTag)
        {
            _context.Routetags.Update(routeTag);
            _context.SaveChanges();
        }
    }
}
