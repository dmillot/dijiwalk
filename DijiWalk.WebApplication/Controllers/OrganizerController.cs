//-----------------------------------------------------------------------
// <copyright file="OrganizerController.cs" company="DijiWalk">
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
    /// Controller for the Organizer
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class OrganizerController : Controller
    {
        /// <summary>
        /// Object private OrganizerRepository with which we will interact with the database
        /// </summary>
        private readonly IOrganizerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizerController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public OrganizerController(IOrganizerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Organizer with his Id
        /// </summary>
        /// <param name="id">Id of the Organizer</param>
        /// <returns>An Organizer</returns>
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
        /// Method to get all Organizer 
        /// </summary>
        /// <returns>A list of Organizer</returns>
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
        /// Method to delete specific organizer
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