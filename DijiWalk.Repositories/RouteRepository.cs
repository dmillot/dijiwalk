//-----------------------------------------------------------------------
// <copyright file="RouteRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Business.Contracts;
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

        private readonly IRouteBusiness _routeBusiness;

        private readonly ITeamBusiness _teamBusiness;

        private readonly IGameRepository _gameRepository;

        private readonly IRouteStepBusiness _routeStepBusiness;

        private readonly IRouteTagBusiness _routeTagBusiness;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public RouteRepository(SmartCityContext context, IGameRepository gameRepository, IRouteBusiness routeBusiness, ITeamBusiness teamBusiness, IRouteStepBusiness routeStepBusiness, IRouteTagBusiness routeTagBusiness)
        {
            _context = context;
            _routeBusiness = routeBusiness;
            _teamBusiness = teamBusiness;
            _gameRepository = gameRepository;
            _routeStepBusiness = routeStepBusiness;
            _routeTagBusiness = routeTagBusiness;
        }


        /// <summary>
        /// Method to Delete from the database the Route passed in the parameters
        /// </summary>
        /// <param name="route">Object Route to Delete</param>
        public async Task<ApiResponse> Delete(int idRoute)
        {
            try
            {
                if (!await _gameRepository.ContainsRoute(idRoute))
                {
                    await _routeBusiness.DeleteAllFromRoute(idRoute);
                    await _teamBusiness.DeleteTeamRoutesFromRoute(idRoute);
                }
                _context.Routes.Remove(await _context.Routes.FindAsync(idRoute));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
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
            return await _context.Routes.Where(r => r.Id == id).Include(r => r.RouteSteps).ThenInclude(rs => rs.Step).Include(r => r.RouteTags).ThenInclude(rt => rt.Tag).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to find all Route from the database
        /// </summary>
        /// <returns>A List with all Routes</returns>
        public async Task<IEnumerable<Route>> FindAll()
        {
            return await _context.Routes.Include(r => r.RouteSteps).ThenInclude(rs => rs.Step).Include(r => r.RouteTags).ThenInclude(rt => rt.Tag).ToListAsync();
        }


        /// <summary>
        /// Method to Add the route passed in the parameters to the database
        /// </summary>
        /// <param name="route">Object Route to Add</param>
        public async Task<ApiResponse> Add(Route route)
        {
            try
            {
                Route newRoute = _routeBusiness.SeparateStep(route);
                await _context.Routes.AddAsync(newRoute);
                await _context.SaveChangesAsync();

                var responseSteps = await _routeStepBusiness.SetUp(route.RouteSteps.ToList(), newRoute.Id);

                if (responseSteps.Status == ApiStatus.Ok)
                {
                    var responseTags = await _routeTagBusiness.SetUp(route.RouteTags.ToList(), newRoute.Id);


                    if (responseTags.Status == ApiStatus.Ok)
                    {
                        return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = await _context.Routes.Where(s => s.Id == newRoute.Id).Include(s => s.RouteSteps).Include(s => s.RouteTags).FirstOrDefaultAsync() };
                    }
                    else
                        return responseTags;
                }
                else
                    return responseSteps;
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method that will Update the route passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Route to Update</param>
        public async Task<ApiResponse> Update(Route route)
        {
            try
            {

                _context.Routes.Update(_routeBusiness.SeparateStep(route));
                await _context.SaveChangesAsync();
                var currentIdRouteStep = await _context.Routesteps.Where(r => r.IdRoute == route.Id).Select(r => r.IdStep).ToListAsync();
                var idRouteStep = route.RouteSteps.Select(r => r.IdStep).ToList();
                var oldIdRouteStep = currentIdRouteStep.Where(r => !idRouteStep.Contains(r)).ToList();
                var responseRouteStepDelete = await _routeStepBusiness.DeleteFromRoute(await _context.Routesteps.Where(m => m.IdRoute == route.Id && oldIdRouteStep.Contains(m.IdStep)).Include(rs => rs.TeamRoutes).ToListAsync());
                if (responseRouteStepDelete.Status == ApiStatus.Ok)
                {
                    var currentIdRouteTag = await _context.Routetags.Where(r => r.IdRoute == route.Id).Select(r => r.IdTag).ToListAsync();
                    var idRouteTag = route.RouteTags.Select(r => r.IdTag).ToList();
                    var oldIdRouteTag = currentIdRouteTag.Where(r => !idRouteTag.Contains(r)).ToList();
                    var responseRouteTagDelete = await _routeTagBusiness.DeleteFromRoute(await _context.Routetags.Where(m => m.IdRoute == route.Id && oldIdRouteTag.Contains(m.IdTag)).ToListAsync());
                    if (responseRouteTagDelete.Status == ApiStatus.Ok)
                    {
                        var responseRouteStepAdd = await _routeStepBusiness.SetUp(route.RouteSteps.Where(m => !currentIdRouteStep.Contains(m.IdStep)).ToList(), route.Id);
                        if (responseRouteStepAdd.Status == ApiStatus.Ok)
                        {
                            var responseRouteTagAdd = await _routeTagBusiness.SetUp(route.RouteTags.Where(m => !currentIdRouteTag.Contains(m.IdTag)).ToList(), route.Id);
                            if (responseRouteTagAdd.Status == ApiStatus.Ok)
                            {
                                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = await this.Find(route.Id) };
                            }
                            else
                                return responseRouteTagAdd;
                        }
                        else
                            return responseRouteStepAdd;
                    }
                    else
                        return responseRouteTagDelete;
                }
                else
                    return responseRouteStepDelete;
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
