﻿//-----------------------------------------------------------------------
// <copyright file="RouteRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Route to the database
    /// </summary>
    public class RouteRepository : IRouteRepository
    {
        private readonly SmartCityContext _context;

        private readonly IRouteStepRepository _routeStepRepository;

        private readonly IRouteTagRepository _routeTagRepository;

        private readonly ITeamRouteRepository _teamRouteRepository;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public RouteRepository(SmartCityContext context, IRouteStepRepository routeStepRepository, IRouteTagRepository routeTagRepository, ITeamRouteRepository teamRouteRepository)
        {
            _context = context;
            _routeStepRepository = routeStepRepository;
            _routeTagRepository = routeTagRepository;
            _teamRouteRepository = teamRouteRepository;
        }
        /// <summary>
        /// Method to Add the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Add</param>
        public void Add(Route route)
        {
           _context.Routes.Add(route);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Route passed in the parameters
        /// </summary>
        /// <param name="route">Object Route to Delete</param>
        public async Task<ApiResponse> Delete(int idRoute)
        {
            try
            {
                //await _routeStepRepository.DeleteAll(idRoute);
                //await _routeTagRepository.DeleteAll(idRoute);
                //await _teamRouteRepository.DeleteAll(idRoute);
                _context.Routes.Remove(await _context.Routes.FindAsync(idRoute));
                _context.SaveChanges();
                return new ApiResponse { Status = 1, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an Route with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Route</param>
        /// <returns>The Route with the Id researched</returns>
        public async Task<Route> Find(int id)
        {
            return await _context.Routes.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Route from the database
        /// </summary>
        /// <returns>A List with all Routes</returns>
        public async Task<IEnumerable<Route>> FindAll()
        {
            return await _context.Routes.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Update</param>
        public void Update(Route route)
        {
           _context.Routes.Update(route);
           _context.SaveChanges();
        }
    }
}
