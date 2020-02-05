//-----------------------------------------------------------------------
// <copyright file="RouteStepController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the RouteStep
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class RouteStepController : Controller
    {
        /// <summary>
        /// Object private RouteStepRepository with which we will interact with the database
        /// </summary>
        private readonly IRouteStepRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteStepController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public RouteStepController(IRouteStepRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a RouteStep with his Id
        /// </summary>
        /// <param name="id">Id of the RouteStep</param>
        /// <returns>A RouteStep</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                return this.Ok(await this._repository.Find(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get all RouteStep 
        /// </summary>
        /// <returns>A list of RouteStep</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return this.Ok(await this._repository.FindAll());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }
    }
}