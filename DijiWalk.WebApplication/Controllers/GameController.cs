//-----------------------------------------------------------------------
// <copyright file="GameController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using DijiWalk.WebApplication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Games
    /// </summary>

    [Route("api/[controller]"), ApiController]
    public class GameController : Controller
    {
        /// <summary>
        /// Object private GameRepository with which we will interact with the database
        /// </summary>
        private readonly IGameRepository _repository;


        /// <summary>
        /// Initializes a new instance of the <see cref="GameController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public GameController(IGameRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Game with his Id
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <returns>An Game</returns>
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
        /// Method to get all Game 
        /// </summary>
        /// <returns>A list of Game</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await this._repository.FindAll());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to add a game 
        /// </summary>
        /// <returns>Success or error message</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game game)
        {
            try
            {
                return this.Ok(await this._repository.Add(game));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to delete a game 
        /// </summary>
        /// <returns>Success or error message</returns>
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
    }
}