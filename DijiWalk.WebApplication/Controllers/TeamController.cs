//-----------------------------------------------------------------------
// <copyright file="TeamController.cs" company="DijiWalk">
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
    /// Controller for the Team
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        /// <summary>
        /// Object private TeamRepository with which we will interact with the database
        /// </summary>
        private readonly ITeamRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TeamController(ITeamRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Team with his Id
        /// </summary>
        /// <param name="id">Id of the Team</param>
        /// <returns>A Team</returns>
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
        /// Method to get all Team 
        /// </summary>
        /// <returns>A list of Team</returns>
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