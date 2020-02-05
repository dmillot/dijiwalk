//-----------------------------------------------------------------------
// <copyright file="TeamRouteController.cs" company="DijiWalk">
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
    /// Controller for the TeamRoute
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class TeamRouteController : Controller
    {
        /// <summary>
        /// Object private TeamRouteRepository with which we will interact with the database
        /// </summary>
        private readonly ITeamRouteRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRouteController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TeamRouteController(ITeamRouteRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a TeamRoute with his Id
        /// </summary>
        /// <param name="id">Id of the TeamRoute</param>
        /// <returns>A TeamRoute</returns>
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
        /// Method to get all TeamRoute 
        /// </summary>
        /// <returns>A list of TeamRoute</returns>
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