//-----------------------------------------------------------------------
// <copyright file="TrialController.cs" company="DijiWalk">
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
    /// Controller for the Trial
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TrialController : Controller
    {
        /// <summary>
        /// Object private TrialRepository with which we will interact with the database
        /// </summary>
        private ITrialRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrialController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TrialController(ITrialRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Trial with his Id
        /// </summary>
        /// <param name="id">Id of the Trial</param>
        /// <returns>A Trial</returns>
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
        /// Method to get all Trial 
        /// </summary>
        /// <returns>A list of Trial</returns>
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