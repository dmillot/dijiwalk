using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class PlayBusiness : IPlayBusiness
    {
        private readonly SmartCityContext _context;

        public PlayBusiness(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add a list of plays passed in the parameters to the database
        /// </summary>
        /// <param name="plays">List of object Play to Add</param>
        public async Task<ApiResponse> AddRange(List<Play> plays)
        {
            try
            {
                await _context.Plays.AddRangeAsync(plays);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = plays };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to set up list of play to add to a game
        /// </summary>
        /// <param name="plays">Object list of play to set up</param>
        /// <param name="idGame">Id of the game</param>
        public async Task<ApiResponse> SetUp(List<Play> plays, int idGame)
        {
            return await this.AddRange(plays.ToList());
        }

        /// <summary>
        /// Method to Delete all plays
        /// </summary>
        /// <param name="plays">List of plays to delete</param>
        public async Task<ApiResponse> DeleteFromGame(List<Play> plays)
        {
            try
            {
                _context.Plays.RemoveRange(plays);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
