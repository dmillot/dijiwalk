//-----------------------------------------------------------------------
// <copyright file="PlayerController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DijiWalk.Business.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Player
    /// </summary>
    [Route("api/[controller]"), ApiController]
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
            _repository = repository;
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

        /// <summary>
        /// Method to get actual game of a player
        /// </summary>
        /// <returns></returns>
        [HttpGet("actual/{id}")]
        public async Task<IActionResult> GetActualGame(int id)
        {
            try
            {
                return this.Ok(await this._repository.GetActualGame(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get all previous games of a player
        /// </summary>
        /// <returns></returns>
        [HttpGet("previous/{id}")]
        public async Task<IActionResult> GetPreviousGames(int id)
        {
            try
            {
                return this.Ok(await this._repository.GetPreviousGames(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get current step of a player
        /// </summary>
        /// <returns></returns>
        [HttpGet("step/{id}")]
        public async Task<IActionResult> GetCurrentStep(int id)
        {
            try
            {
                return this.Ok(await this._repository.GetCurrentStep(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to add player
        /// </summary>
        /// <returns>Success or error message</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Player player)
        {
            try
            {
                return this.Ok(await _repository.Add(player));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to delete specific player
        /// </summary>
        /// <returns>Message action</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return this.Ok(await this._repository.Delete(id));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Update a Player 
        /// </summary>
        /// <returns>A Player</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Player player)
        {
            try
            {
                player.Id = id;
                return this.Ok(await _repository.Update(player));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }
    }
}