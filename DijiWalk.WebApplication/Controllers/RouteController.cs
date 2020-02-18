//-----------------------------------------------------------------------
// <copyright file="RouteController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Route
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class RouteController : Controller
    {
        /// <summary>
        /// Object private RouteRepository with which we will interact with the database
        /// </summary>
        private readonly IRouteRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public RouteController(IRouteRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Route with his Id
        /// </summary>
        /// <param name="id">Id of the Route</param>
        /// <returns>A Route</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return this.Ok(await _repository.Find(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get all Route 
        /// </summary>
        /// <returns>A list of Route</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var routes = await this._repository.FindAll();
                return this.Ok(
                    routes.Select(route =>
                    {
                        route.RouteTags = route.RouteTags.Select(rt =>
                        {
                            rt.Route = null;
                            rt.Tag.RouteTags = new HashSet<RouteTag>();
                            rt.Tag.StepTags = new HashSet<StepTag>();
                            return rt;
                        }).ToList();
                        route.RouteSteps = route.RouteSteps.Select(rs =>
                        {
                            rs.Route = null;
                            rs.TeamRoutes = new HashSet<TeamRoute>();
                            rs.Step.RouteSteps = new HashSet<RouteStep>();
                            rs.Step.StepTags = new HashSet<StepTag>();
                            rs.Step.StepValidations = new HashSet<StepValidation>();
                            rs.Step.Missions = new HashSet<Mission>();
                            rs.Step.Clues = new HashSet<Clue>();
                            return rs;
                        }).ToList();
                        return route;
                    }).ToList()

                );
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to delete specific route
        /// </summary>
        /// <returns>Message action</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return this.Ok(await this._repository.Delete(id));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Add a Step 
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Route route)
        {
            try
            {
                return this.Ok(await _repository.Add(route));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Update a Step 
        /// </summary>
        /// <returns>A Step</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Route route)
        {
            try
            {
                route.Id = id;
                return this.Ok(await _repository.Update(route));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

    }
}