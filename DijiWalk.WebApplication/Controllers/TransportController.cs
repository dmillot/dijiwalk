//-----------------------------------------------------------------------
// <copyright file="TransportController.cs" company="DijiWalk">
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
    /// Controller for the Transport
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : Controller
    {
        /// <summary>
        /// Object private TransportRepository with which we will interact with the database
        /// </summary>
        private ITransportRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransportController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TransportController(ITransportRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Transport with his Id
        /// </summary>
        /// <param name="id">Id of the Transport</param>
        /// <returns>A Transport</returns>
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
        /// Method to get all Transport 
        /// </summary>
        /// <returns>A list of Transport</returns>
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