//-----------------------------------------------------------------------
// <copyright file="TeamPlayerController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the TeamPlayer
    /// </summary>
    [Route("api/[controller]"), ApiController]
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
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return this.Ok(await this._repository.FindByTeam(id));
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
        /// Method to delete specific team player
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
    }
}