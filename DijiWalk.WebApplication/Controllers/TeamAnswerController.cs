//-----------------------------------------------------------------------
// <copyright file="TeamAnswerController.cs" company="DijiWalk">
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
    /// Controller for the Route
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
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
        /// Method to get all TeamAnswer 
        /// </summary>
        /// <returns>A list of TeamAnswer</returns>
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
        /// Method to delete specific team answer
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