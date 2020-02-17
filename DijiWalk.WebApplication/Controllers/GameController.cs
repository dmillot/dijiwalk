//-----------------------------------------------------------------------
// <copyright file="GameController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using DijiWalk.WebApplication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Games
    /// </summary>

    [Route("api/[controller]"), ApiController]
    public class GameController : Controller
    {
        /// <summary>
        /// Object private GameRepository with which we will interact with the database
        /// </summary>
        private readonly IGameRepository _repository;


        /// <summary>
        /// Initializes a new instance of the <see cref="GameController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public GameController(IGameRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Method to get an Game with his Id
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <returns>An Game</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return this.Ok(await this._repository.Find(id));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }

        }

        /// <summary>
        /// Method to Update a game 
        /// </summary>
        /// <returns>A game</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game)
        {
            try
            {
                game.Id = id;
                return this.Ok(await _repository.Update(game));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to get all Game 
        /// </summary>
        /// <returns>A list of Game</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var games = await this._repository.FindAll();
                return Ok(games.Select(g =>
                {
                    if (g.Organizer != null)
                    {
                        g.Organizer.Games = new HashSet<Game>();
                        g.Organizer.Messages = new HashSet<Message>();
                        g.Organizer.Players = new HashSet<Player>();
                        g.Organizer.Routes = new HashSet<Route>();
                        g.Organizer.Teams = new HashSet<Team>();
                    }
                    if (g.Route != null)
                    {
                        g.Route.Games = new HashSet<Game>();
                        g.Route.RouteSteps = new HashSet<RouteStep>();
                        g.Route.RouteTags = new HashSet<RouteTag>();
                    }
                    if (g.Transport != null)
                    {
                        g.Transport.Games = new HashSet<Game>();
                    }

                    if (g.Plays != null)
                    {
                        g.Plays = g.Plays.Select(p =>
                        {
                            p.Game = null;
                            p.Team.Organizer = null;
                            p.Team.Plays = new HashSet<Play>();
                            p.Team.TeamAnswers = new HashSet<TeamAnswer>();
                            p.Team.TeamPlayers = new HashSet<TeamPlayer>();
                            p.Team.TeamRoutes = new HashSet<TeamRoute>();
                            return p;
                        }).ToList();
                    }
                    return g;
                   
                }).ToList());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get all Game 
        /// </summary>
        /// <returns>A list of Game</returns>
        [HttpGet, Route("actifs")]
        public async Task<IActionResult> GetAllActifs()
        {
            try
            {
                var games = await this._repository.FindAllActifs();
                return Ok(games.Select(g =>
                {
                    if (g.Organizer != null)
                    {
                        g.Organizer.Games = new HashSet<Game>();
                        g.Organizer.Messages = new HashSet<Message>();
                        g.Organizer.Players = new HashSet<Player>();
                        g.Organizer.Routes = new HashSet<Route>();
                        g.Organizer.Teams = new HashSet<Team>();
                    }
                    if (g.Route != null)
                    {
                        g.Route.Games = new HashSet<Game>();
                        g.Route.RouteSteps = g.Route.RouteSteps.Select(rs =>
                        {
                            rs.Route = null;
                            rs.Step.RouteSteps = new HashSet<RouteStep>();
                            rs.Step.StepTags = new HashSet<StepTag>();
                            rs.Step.StepTags = new HashSet<StepTag>();
                            rs.Step.StepValidations = new HashSet<StepValidation>();
                            rs.Step.Missions = new HashSet<Mission>();
                            rs.Step.Clues = new HashSet<Clue>();
                            return rs;
                        }).ToList();
                        g.Route.RouteTags = new HashSet<RouteTag>();
                    }
                   
                    return g;

                }).ToList());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to add a game 
        /// </summary>
        /// <returns>Success or error message</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Game game)
        {
            try
            {
                return this.Ok(await this._repository.Add(game));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to delete a game 
        /// </summary>
        /// <returns>Success or error message</returns>
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