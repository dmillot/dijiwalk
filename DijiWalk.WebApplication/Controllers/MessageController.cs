﻿//-----------------------------------------------------------------------
// <copyright file="MessageController.cs" company="DijiWalk">
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
    /// Controller for the Message
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        /// <summary>
        /// Object private MessageRepository with which we will interact with the database
        /// </summary>
        private IMessageRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public MessageController(IMessageRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Message with his Id
        /// </summary>
        /// <param name="id">Id of the Message</param>
        /// <returns>An Message</returns>
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
        /// Method to get all Message 
        /// </summary>
        /// <returns>A list of Message</returns>
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