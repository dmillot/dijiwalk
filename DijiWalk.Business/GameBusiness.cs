using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DijiWalk.Business
{
    public class GameBusiness : IGameBusiness
    {
        private readonly SmartCityContext _context;

        public GameBusiness(SmartCityContext context)
        {
            this._context = context;
        }

        public bool IsGameInProgress(Game game)
        {
            return Convert.ToDateTime(game.CreationDate).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy");
        }

        public async Task<ApiResponse> DeleteAllTeams(Game game)
        {
            try
            {
                _context.Plays.RemoveRange(await _context.Plays.Where(p => p.IdGame == game.Id).ToListAsync());
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to separate plays and game 
        /// </summary>
        /// <param name="game">Object game</param>
        public Game SeparatePlay(Game game)
        {
            return new Game(game);
        }

    }
}
