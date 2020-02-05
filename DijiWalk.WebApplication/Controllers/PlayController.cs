//-----------------------------------------------------------------------
// <copyright file="PlayController.cs" company="DijiWalk">
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
    /// Controller for the Administrator
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class PlayController : Controller
    {
        /// <summary>
        /// Object private PlayRepository with which we will interact with the database
        /// </summary>
        private IPlayRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public PlayController(IPlayRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Play with his Id
        /// </summary>
        /// <param name="id">Id of the Administrator</param>
        /// <returns>A Play</returns>
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
        /// Method to get all Play 
        /// </summary>
        /// <returns>A list of Play</returns>
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