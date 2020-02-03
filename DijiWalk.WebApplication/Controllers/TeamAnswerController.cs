//-----------------------------------------------------------------------
// <copyright file="TeamAnswerController.cs" company="DijiWalk">
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
    [Route("api/[controller]")]
    [ApiController]
    public class TeamAnswerController : Controller
    {
        /// <summary>
        /// Object private TeamAnswerRepository with which we will interact with the database
        /// </summary>
        private readonly ITeamAnswerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamAnswerController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TeamAnswerController(ITeamAnswerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a TeamAnswer with his Id
        /// </summary>
        /// <param name="id">Id of the TeamAnswer</param>
        /// <returns>A TeamAnswer</returns>
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
        /// Method to get all TeamAnswer 
        /// </summary>
        /// <returns>A list of TeamAnswer</returns>
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