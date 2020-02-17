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
        public async Task<Game> GetActualGame(int idPlayer)
        {
            var actualGame = (from game in _context.Games
                        join play in _context.Plays
                         on game.Id equals play.IdGame
                        join teamplayer in _context.Teamplayers
                         on play.IdTeam equals teamplayer.IdTeam
                        join player in _context.Players
                         on teamplayer.IdPlayer equals player.Id
                        where DateTime.Now >= play.StartDate && DateTime.Now <= play.EndDate && player.Id == idPlayer
                        select game).FirstOrDefault();

            return actualGame;
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
            Step currentStep = new Step();
            var actualGame = await this.GetActualGame(idPlayer);
            if (actualGame != null)
            {
                var currentTeam = await _context.Teamplayers.Where(t => t.IdPlayer == idPlayer).Select(x => x.IdTeam).FirstOrDefaultAsync();
                var allSteps = await _context.Teamroutes.Include(rt => rt.RouteStep).ThenInclude(s => s.Step).Where(t => t.IdGame == actualGame.Id && t.IdTeam == currentTeam).OrderBy(x => x.StepOrder).ToListAsync();
                foreach (var item in allSteps)
                {
                    if (item.ValidationDate == null)
                    {
                        currentStep = item.RouteStep.Step;
                        break;
                    }
                }
            }

            return currentStep;
        }
    }
}