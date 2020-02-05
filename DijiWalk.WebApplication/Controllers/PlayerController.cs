//-----------------------------------------------------------------------
// <copyright file="PlayerController.cs" company="DijiWalk">
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
    /// Controller for the Player
    /// </summary>
    [Route("api/[controller]"), Authorize, ApiController]
    public class PlayerController : Controller
    {
        /// <summary>
        /// Object private PlayerRepository with which we will interact with the database
        /// </summary>
        private readonly IPlayerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public PlayerController(IPlayerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Player with his Id
        /// </summary>
        /// <param name="id">Id of the Player</param>
        /// <returns>A Player</returns>
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
        /// Method to get all Player 
        /// </summary>
        /// <returns>A list of Player</returns>
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