//-----------------------------------------------------------------------
// <copyright file="OrganizerController.cs" company="DijiWalk">
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
    /// Controller for the Organizer
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : Controller
    {
        /// <summary>
        /// Object private OrganizerRepository with which we will interact with the database
        /// </summary>
        private IOrganizerRepository _repository;

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
        /// Method to get all Organizer 
        /// </summary>
        /// <returns>A list of Organizer</returns>
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