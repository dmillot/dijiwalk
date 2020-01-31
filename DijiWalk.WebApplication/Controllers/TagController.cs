//-----------------------------------------------------------------------
// <copyright file="TagController.cs" company="DijiWalk">
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
    /// Controller for the Route
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        /// <summary>
        /// Object private TagRepository with which we will interact with the database
        /// </summary>
        private ITagRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TagController(ITagRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Tag with his Id
        /// </summary>
        /// <param name="id">Id of the Tag</param>
        /// <returns>A Tag</returns>
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
        /// Method to get all Tag 
        /// </summary>
        /// <returns>A list of Tag</returns>
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