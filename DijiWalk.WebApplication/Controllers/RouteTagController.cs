//-----------------------------------------------------------------------
// <copyright file="RouteTagController.cs" company="DijiWalk">
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
    /// Controller for the RouteTag
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class RouteTagController : Controller
    {
        /// <summary>
        /// Object private RouteTagRepository with which we will interact with the database
        /// </summary>
        private readonly IRouteTagRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTagController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public RouteTagController(IRouteTagRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a RouteTag with his Id
        /// </summary>
        /// <param name="id">Id of the RouteTag</param>
        /// <returns>A RouteTag</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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
        /// Method to get all RouteTag 
        /// </summary>
        /// <returns>A list of RouteTag</returns>
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