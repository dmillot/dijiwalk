//-----------------------------------------------------------------------
// <copyright file="TeamController.cs" company="DijiWalk">
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
    /// Controller for the Team
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class TeamController : Controller
    {
        /// <summary>
        /// Object private TeamRepository with which we will interact with the database
        /// </summary>
        private readonly ITeamRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TeamController(ITeamRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a Team with his Id
        /// </summary>
        /// <param name="id">Id of the Team</param>
        /// <returns>A Team</returns>
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
        /// Method to get all Team 
        /// </summary>
        /// <returns>A list of Team</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teams = await this._repository.FindAll();
                return Ok(teams.Select(t =>
                {
                    t.Plays = t.Plays.Select(p =>
                    {
                        p.Team = null;
                        p.Game.Plays = new HashSet<Play>();
                        p.Game.TeamAnswers = new HashSet<TeamAnswer>();
                        p.Game.TeamRoutes = new HashSet<TeamRoute>();
                        p.Game.Organizer = null;
                        p.Game.Route = null;
                        p.Game.Transport = null;
                        return p;
                    }).ToList();
                    t.Captain.Messages = new HashSet<Message>();
                    t.Captain.Teams = new HashSet<Team>();
                    t.Captain.TeamPlayers = new HashSet<TeamPlayer>();
                    t.Captain.Organizer = null;
                    t.TeamAnswers = new HashSet<TeamAnswer>();
                    t.TeamRoutes = new HashSet<TeamRoute>();
                    t.TeamPlayers = t.TeamPlayers.Select(tp =>
                    {
                        tp.Player.Messages = new HashSet<Message>();
                        tp.Player.Teams = new HashSet<Team>();
                        tp.Player.TeamPlayers = new HashSet<TeamPlayer>();
                        tp.Player.Organizer = null;
                        tp.Team = null;
                        return tp;
                    }).ToList();
                    t.Organizer = null;
                    return t;
                }).ToList());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to delete specific team
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

        /// <summary>
        /// Method to Add a team 
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Team team)
        {
            try
            {
                return this.Ok(await _repository.Add(team));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Update a team 
        /// </summary>
        /// <returns>A team</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Team team)
        {
            try
            {
                team.Id = id;
                return this.Ok(await _repository.Update(team));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }
    }
}