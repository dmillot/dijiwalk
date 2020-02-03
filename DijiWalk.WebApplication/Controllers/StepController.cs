//-----------------------------------------------------------------------
// <copyright file="StepController.cs" company="DijiWalk">
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
    /// Controller for the Step
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : Controller
    {
        /// <summary>
        /// Object private StepRepository with which we will interact with the database
        /// </summary>
        private readonly IStepRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public StepController(IStepRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Step with his Id
        /// </summary>
        /// <param name="id">Id of the Step</param>
        /// <returns>A Step</returns>
        [HttpGet("{id}"), AllowAnonymous]
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
        /// Method to get all Step 
        /// </summary>
        /// <returns>A list of Step</returns>
        [HttpGet, AllowAnonymous]
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