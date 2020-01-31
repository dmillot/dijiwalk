//-----------------------------------------------------------------------
// <copyright file="AnswerController.cs" company="DijiWalk">
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
    /// Controller for the Answer
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : Controller
    {
        /// <summary>
        /// Object private AnswerRepository with which we will interact with the database
        /// </summary>
        private IAnswerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public AnswerController(IAnswerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Answer with his Id
        /// </summary>
        /// <param name="id">Id of the Answer</param>
        /// <returns>An Answer</returns>
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
        /// Method to get all Answer
        /// </summary>
        /// <returns>A list of Answer</returns>
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