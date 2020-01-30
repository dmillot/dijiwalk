//-----------------------------------------------------------------------
// <copyright file="GameController.cs" company="DijiWalk">
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
    /// Controller for the Game
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        /// <summary>
        /// Object private GameRepository with which we will interact with the database
        /// </summary>
        private IGameRepository _repository;

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
        public IActionResult Get(int id)
        {
            try
            {
                return this.Ok(JsonConvert.SerializeObject(this._repository.Find(id), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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
        public IActionResult GetAll()
        {
            try
            {
                return this.Ok(JsonConvert.SerializeObject(this._repository.FindAll(), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }
    }
}