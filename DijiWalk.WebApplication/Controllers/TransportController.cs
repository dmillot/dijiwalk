//-----------------------------------------------------------------------
// <copyright file="TransportController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for the Transport
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class TransportController : Controller
    {
        /// <summary>
        /// Object private TransportRepository with which we will interact with the database
        /// </summary>
        private readonly ITransportRepository _repository;


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
        /// Method to get all Transport 
        /// </summary>
        /// <returns>A list of Transport</returns>
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
        /// Method to Add a Transport 
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Transport transport)
        {
            try
            {
                //this._repository.Add(transport);
                return this.Ok(await this._repository.Add(transport));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to Update a Transport 
        /// </summary>
        /// <returns>A Step</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Transport transport)
        {
            try
            {
                transport.Id = id;
                
                return this.Ok(await this._repository.Update(transport));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to get all Transport 
        /// </summary>
        /// <returns>A list of Transport</returns>
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