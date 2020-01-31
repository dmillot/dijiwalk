﻿//-----------------------------------------------------------------------
// <copyright file="TypeController.cs" company="DijiWalk">
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
    /// Controller for the Type
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        /// <summary>
        /// Object private TypeRepository with which we will interact with the database
        /// </summary>
        private ITypeRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TypeController(ITypeRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Type with his Id
        /// </summary>
        /// <param name="id">Id of the Type</param>
        /// <returns>A Type</returns>
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
        /// Method to get all Type 
        /// </summary>
        /// <returns>A list of Type</returns>
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