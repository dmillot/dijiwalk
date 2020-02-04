//-----------------------------------------------------------------------
// <copyright file="RouteController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Route
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
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
        public IActionResult Get(int id)
        {
            try
            {
                return this.Ok(this._repository.Find(id));
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
        public IActionResult GetAll()
        {
            try
            {
                return this.Ok(this._repository.FindAll());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }
    }
}