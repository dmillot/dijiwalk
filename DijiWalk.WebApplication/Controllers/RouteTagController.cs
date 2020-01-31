//-----------------------------------------------------------------------
// <copyright file="RouteTagController.cs" company="DijiWalk">
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
    /// Controller for the RouteTag
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RouteTagController : Controller
    {
        /// <summary>
        /// Object private RouteTagRepository with which we will interact with the database
        /// </summary>
        private IRouteTagRepository _repository;

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
        /// Method to get all RouteTag 
        /// </summary>
        /// <returns>A list of RouteTag</returns>
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