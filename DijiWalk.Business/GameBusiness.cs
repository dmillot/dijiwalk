using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DijiWalk.Common.ArrayShuffle;
using Microsoft.Data.SqlClient;

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
            return Convert.ToDateTime(game.CreationDate) <= DateTime.Now;
        }

        public async Task<ApiResponse> DeleteAllTeams(Game game)
        {
            try
            {
                _context.Plays.RemoveRange(await _context.Plays.Where(p => p.IdGame == game.Id).ToListAsync());
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        public async Task<ApiResponse> DeleteAllTeamsRoute(int gameId)
        {
            try
            {
                _context.Teamroutes.RemoveRange(await _context.Teamroutes.Where(p => p.IdGame == gameId).ToListAsync());
                await _context.SaveChangesAsync();
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


        /// <summary>
        /// Generate team route differently and random
        /// </summary>
        /// <param name="game">Object game</param>
        /// <param name="idGame">Id of the game</param>
        public async Task<ApiResponse> GenerateTeamRoute(Game game, int idGame)
        {

            try
            {
                var nbOrder = new List<int>();
                var listIdStep = game.Route.RouteSteps.ToList().Select(rs =>
                {
                    nbOrder.Add(nbOrder.Count + 1);
                    return rs.IdStep;
                }).ToList();
                var listIdTeam = game.Plays.ToList().Select(p => p.IdTeam).ToList();
                var listOrder = nbOrder.Shuffle(game.Plays.Count());
                var listTeamRoute = new List<TeamRoute>();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.TEAMROUTE ON");
                foreach (var l in listOrder)
                {
                    foreach (var order in l)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO [dbo].[TEAMROUTE] ([TeamRoute_fk_Game_Id], [TeamRoute_fk_Team_Id], [TeamRoute_fk_Route_Id], [TeamRoute_fk_Step_Id], [TeamRoute_StepOrder], [TeamRoute_Validate]) VALUES ({idGame}, {listIdTeam[listOrder.IndexOf(l)]}, {game.IdRoute}, {listIdStep[l.IndexOf(order)]}, {order}, {false})");
                    }
                }
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.TEAMROUTE OFF");
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
