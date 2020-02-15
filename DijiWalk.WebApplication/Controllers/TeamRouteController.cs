//-----------------------------------------------------------------------
// <copyright file="TeamRouteController.cs" company="DijiWalk">
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
    /// Controller for the TeamRoute
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class TeamRouteController : Controller
    {
        /// <summary>
        /// Object private TeamRouteRepository with which we will interact with the database
        /// </summary>
        private readonly ITeamRouteRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRouteController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public TeamRouteController(ITeamRouteRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get a TeamRoute with his Id
        /// </summary>
        /// <param name="id">Id of the TeamRoute</param>
        /// <returns>A TeamRoute</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var teamRoutes = await this._repository.FindInGame(id);
                return Ok(teamRoutes.Select(tr =>
                {
                    tr.Game.Organizer = null;
                    tr.Game.Plays = new HashSet<Play>();
                    tr.Game.TeamAnswers = new HashSet<TeamAnswer>();
                    tr.Game.TeamRoutes = new HashSet<TeamRoute>();
                    tr.Game.Route = null;
                    tr.Game.Transport = null;

                    tr.Team.Captain.Organizer = null;
                    tr.Team.Captain.Messages = new HashSet<Message>();
                    tr.Team.Captain.Teams = new HashSet<Team>();
                    tr.Team.Captain.TeamPlayers = new HashSet<TeamPlayer>(); ;

                    tr.Team.Plays = new HashSet<Play>();
                    tr.Team.TeamAnswers = new HashSet<TeamAnswer>();
                    tr.Team.TeamPlayers = new HashSet<TeamPlayer>();
                    tr.Team.TeamRoutes = new HashSet<TeamRoute>();
                    tr.Team.Organizer = null;

                    tr.RouteStep.TeamRoutes = new HashSet<TeamRoute>();
                    tr.RouteStep.Route.Games = new HashSet<Game>();
                    tr.RouteStep.Route.RouteSteps = new HashSet<RouteStep>();
                    tr.RouteStep.Route.RouteTags = new HashSet<RouteTag>();
                    tr.RouteStep.Route.Organizer = null;

                    tr.RouteStep.Step.Missions = new HashSet<Mission>();
                    tr.RouteStep.Step.RouteSteps = new HashSet<RouteStep>();
                    tr.RouteStep.Step.StepTags = new HashSet<StepTag>();
                    tr.RouteStep.Step.StepValidations = new HashSet<StepValidation>();
                    return tr;

                }).ToList());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get all TeamRoute 
        /// </summary>
        /// <returns>A list of TeamRoute</returns>
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
        /// Method to delete specific team route
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
        /// Method to valid a teamroute
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost, Route("valid")]
        public async Task<IActionResult> Valid(TeamRoute teamRoute)
        {
            try
            {
                return this.Ok(await _repository.Update(teamRoute, true));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to reject a teamroute
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost, Route("reject")]
        public async Task<IActionResult> Reject(TeamRoute teamRoute)
        {
            try
            {
                return this.Ok(await _repository.Update(teamRoute, false));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }
    }
}