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

        public async Task<IEnumerable<Game>> GetPreviousGames(int idPlayer)
        {

            var test = from game in _context.Games
                       join play in _context.Plays
                        on game.Id equals play.IdGame
                       join teamplayer in _context.Teamplayers
                        on play.IdTeam equals teamplayer.IdTeam
                       join player in _context.Players
                        on teamplayer.IdPlayer equals player.Id
                       where game.CreationDate < DateTime.Now
                       where player.Id == idPlayer
                       select game;

            return test;
        }
    }
}