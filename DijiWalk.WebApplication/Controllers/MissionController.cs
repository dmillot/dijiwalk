//-----------------------------------------------------------------------
// <copyright file="MissionController.cs" company="DijiWalk">
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
    /// Controller for the Mission
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class MissionController : Controller
    {
        /// <summary>
        /// Object private MissionRepository with which we will interact with the database
        /// </summary>
        private readonly IMissionRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MissionController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public MissionController(IMissionRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Mission with his Id
        /// </summary>
        /// <param name="id">Id of the Mission</param>
        /// <returns>A Mission</returns>
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
        /// Method to get all Mission 
        /// </summary>
        /// <returns>A list of Mission</returns>
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