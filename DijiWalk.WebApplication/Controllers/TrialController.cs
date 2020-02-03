//-----------------------------------------------------------------------
// <copyright file="TrialController.cs" company="DijiWalk">
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
    /// Controller for the Trial
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TrialController : Controller
    {
        /// <summary>
        /// Object private TrialRepository with which we will interact with the database
        /// </summary>
        private ITrialRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrialController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TrialController(ITrialRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Trial with his Id
        /// </summary>
        /// <param name="id">Id of the Trial</param>
        /// <returns>A Trial</returns>
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
        /// Method to get all Trial 
        /// </summary>
        /// <returns>A list of Trial</returns>
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