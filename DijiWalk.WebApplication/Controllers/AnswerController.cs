//-----------------------------------------------------------------------
// <copyright file="AnswerController.cs" company="DijiWalk">
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
    /// Controller for the Answer
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
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
        /// Method to get all Answer
        /// </summary>
        /// <returns>A list of Answer</returns>
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