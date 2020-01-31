//-----------------------------------------------------------------------
// <copyright file="RouteStepController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the RouteStep
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RouteStepController : Controller
    {
        /// <summary>
        /// Object private RouteStepRepository with which we will interact with the database
        /// </summary>
        private IRouteStepRepository _repository;

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
        public IActionResult Get(int id)
        {
            try
            {
                return this.Ok(JsonConvert.SerializeObject(this._repository.Find(id), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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
        public IActionResult GetAll()
        {
            try
            {
                return this.Ok(JsonConvert.SerializeObject(this._repository.FindAll(), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }
    }
}