//-----------------------------------------------------------------------
// <copyright file="AdministratorController.cs" company="DijiWalk">
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
    /// Controller for the Administrator
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class AdministratorController : Controller
    {
        /// <summary>
        /// Object private AdministratorRepository with which we will interact with the database
        /// </summary>
        private IAdministratorRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministratorController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public AdministratorController(IAdministratorRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Administrator with his Id
        /// </summary>
        /// <param name="id">Id of the Administrator</param>
        /// <returns>An Administrator</returns>
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
        /// Method to get all Administrator 
        /// </summary>
        /// <returns>A list of Administrator</returns>
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
        /// Method to delete specific administrator
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