using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class PlayerBusiness : IPlayerBusiness
    {

        private readonly SmartCityContext _context;

        public PlayerBusiness(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to change check if login and email are unique
        /// </summary>
        /// <param name="player">Object player</param>
        public async Task<bool> Check(Player player)
        {
            return await _context.Players.AnyAsync(x => x.Email == player.Email || x.Login == player.Login);
        }
        /// <summary>
        /// Method to change check if login and email are unique but not check for the player updated
        /// </summary>
        /// <param name="player">Object player</param>
        public async Task<bool> CheckUpdate(Player player)
        {
            return await _context.Players.Where(x => x.Id != player.Id).AnyAsync(x => x.Email == player.Email || x.Login == player.Login);
        }

        /// <summary>
        /// Method to get actual game of a player.
        /// </summary>
        /// <param name="player">Id of the player</param>
        /// <returns>Actual game</returns>
        public async Task<List<Game>> GetMobileInfo(int idPlayer)
        {
            var games = new List<Game>();
            var idTeams = await _context.Teamplayers.Where(tp => tp.IdPlayer == idPlayer).Select(tp => tp.IdTeam).ToListAsync();
            var idGames = await _context.Plays.Where(p => idTeams.Contains(p.IdTeam)).Select(g => g.IdGame).ToListAsync();
            var currentGame = await _context.Games.Where(g => idGames.Contains(g.Id) && DateTime.Now >= g.CreationDate && g.FinalTime == null)
                .Include(tr => tr.TeamRoutes).ThenInclude(rs => rs.RouteStep)
                .Include(p => p.Plays).ThenInclude(t => t.Team)
                .Include(r => r.Route).ThenInclude(rs => rs.RouteSteps).ThenInclude(s => s.Step)
                .FirstOrDefaultAsync();
            var idCurrentGame = currentGame.Id;
            currentGame.TeamRoutes = currentGame.TeamRoutes.Where(tr => idTeams.Contains(tr.IdTeam)).OrderBy(x => x.StepOrder).Select(t => {
                t.Game = null;
                t.Team = null;
                t.RouteStep = null;
                return t;
            }).ToList();

            currentGame.Plays = currentGame.Plays.Select(p => {
                p.Game = null;
                if (idTeams.Contains(p.IdTeam))
                {

                    p.Team.TeamAnswers = new HashSet<TeamAnswer>();
                    p.Team.TeamPlayers = new HashSet<TeamPlayer>();
                    p.Team.TeamRoutes = new HashSet<TeamRoute>();
                    p.Team.Organizer = null;
                    p.Team.Plays = new HashSet<Play>();
                    p.Team.Captain = null;
                } else
                {
                    p.Team = null;
                }
                return p;
            }).ToList();

            currentGame.Plays = currentGame.Plays.Where(p => p.Team != null).ToList();

            currentGame.Route.Organizer = null;
            currentGame.Route.RouteTags = new HashSet<RouteTag>();
            currentGame.Route.Games = new HashSet<Game>();
            currentGame.Route.RouteSteps = currentGame.Route.RouteSteps.Select(rs =>
            {
                rs.Route = null;
                rs.TeamRoutes = null;
                rs.Step.StepTags = new HashSet<StepTag>();
                rs.Step.RouteSteps = new HashSet<RouteStep>();
                rs.Step.StepValidations = new HashSet<StepValidation>();
                rs.Step.Missions = new HashSet<Mission>();
                rs.TeamRoutes = new HashSet<TeamRoute>();
                rs.Route = null;
                return rs;
            }).ToList();
            games.Add(currentGame);
            var gamesPrevious = await _context.Games.Where(g => idGames.Contains(g.Id) && idCurrentGame != g.Id).ToListAsync();
            
            games.AddRange(gamesPrevious.Select(gp => {
                gp.Organizer = null;
                gp.Route = null;
                gp.TeamAnswers = new HashSet<TeamAnswer>();
                gp.TeamRoutes = new HashSet<TeamRoute>();
                gp.Transport = null;
                gp.Plays = new HashSet<Play>();
                return gp;
            }).ToList());
            return games;
        }

        /// <summary>
        /// Method to get all previous games of a player.
        /// </summary>
        /// <param name="player">Id of the player</param>
        /// <returns>List of previous games</returns>
        public async Task<IEnumerable<Game>> GetPreviousGames(int idPlayer)
        {

            var games = from game in _context.Games
                       join play in _context.Plays
                        on game.Id equals play.IdGame
                       join teamplayer in _context.Teamplayers
                        on play.IdTeam equals teamplayer.IdTeam
                       join player in _context.Players
                        on teamplayer.IdPlayer equals player.Id
                       where game.CreationDate < DateTime.Now && player.Id == idPlayer
                       select game;

            return games;
        }

        /// <summary>
        /// Method to get current step of a player.
        /// </summary>
        /// <param name="player">Id of the player</param>
        /// <returns>Current step</returns>
        public async Task<Step> GetCurrentStep(int idPlayer)
        {
            //Step currentStep = new Step();
            //var actualGame = await this.GetActualGame(idPlayer);
            //if (actualGame != null)
            //{
            //    var currentTeam = await _context.Teamplayers.Where(t => t.IdPlayer == idPlayer).Select(x => x.IdTeam).FirstOrDefaultAsync();

            //    var allSteps = await _context.Teamroutes
            //        .Where(t => t.IdGame == actualGame.Id && t.IdTeam == currentTeam)
            //        .Include(rt => rt.RouteStep).ThenInclude(s => s.Step)
            //        .OrderBy(x => x.StepOrder)
            //        .ToListAsync();

            //    foreach (var item in allSteps)
            //    {
            //        if (item.ValidationDate == null)
            //        {
            //            currentStep = item.RouteStep.Step;
            //            break;
            //        }
            //    }
            //}

            return new Step();
        }
    }
}