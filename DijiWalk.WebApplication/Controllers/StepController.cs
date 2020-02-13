//-----------------------------------------------------------------------
// <copyright file="StepController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Step
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class StepController : Controller
    {
        /// <summary>
        /// Object private StepRepository with which we will interact with the database
        /// </summary>
        private readonly IStepRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public StepController(IStepRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Step with his Id
        /// </summary>
        /// <param name="id">Id of the Step</param>
        /// <returns>A Step</returns>
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
        /// Method to get all Step 
        /// </summary>
        /// <returns>A list of Step</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var step = await this._repository.FindAll();
                return Ok(step.Select(g =>
                {
                    g.Missions = g.Missions.Select(m =>
                    {
                        m.Step = null;
                        m.Trials = m.Trials.Select(t =>
                        {
                            t.Answers = new HashSet<Answer>();
                            t.TeamAnswers = new HashSet<TeamAnswer>();
                            t.Mission = null;
                            t.Type = null;
                            t.CorrectAnswer = null;
                            return t;
                        }).ToList();
                        return m;
                    }).ToList();
                    g.RouteSteps = g.RouteSteps.Select(rs =>
                    {
                        rs.Route.Games = new HashSet<Game>();
                        rs.Route.RouteSteps = new HashSet<RouteStep>();
                        rs.Route.RouteTags = new HashSet<RouteTag>();
                        rs.Route.Organizer = null;
                        return rs;
                    }).ToList();
                    g.StepTags = g.StepTags.Select(st =>
                    {
                        st.Step = null;
                        st.Tag.StepTags = new HashSet<StepTag>();
                        st.Tag.RouteTags = new HashSet<RouteTag>();
                        return st;
                    }).ToList();
                    return g;
                }).ToList());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to Add a Step 
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Step step)
        {
            try
            {
                return this.Ok(await _repository.Add(step));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Update a Step 
        /// </summary>
        /// <returns>A Step</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Step step)
        {
            try
            {
                step.Id = id;
                return this.Ok(await _repository.Update(step));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Delete a Step 
        /// </summary>
        /// <returns>Ok Object</returns>
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