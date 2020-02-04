//-----------------------------------------------------------------------
// <copyright file="TeamPlayerController.cs" company="DijiWalk">
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
    /// Controller for the TeamPlayer
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class TeamPlayerController : Controller
    {
        /// <summary>
        /// Object private TeamPlayerRepository with which we will interact with the database
        /// </summary>
        private readonly ITeamPlayerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamPlayerController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TeamPlayerController(ITeamPlayerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a TeamPlayer with his Id
        /// </summary>
        /// <param name="id">Id of the TeamPlayer</param>
        /// <returns>A TeamPlayer</returns>
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
        /// Method to get all TeamPlayer 
        /// </summary>
        /// <returns>A list of TeamPlayer</returns>
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