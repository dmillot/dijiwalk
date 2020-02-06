using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class CapitaineBusiness : ICapitaineBusiness
    {

        private readonly SmartCityContext _context;

        public CapitaineBusiness(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to change the capitaine if the player was the capitaine in his team
        /// </summary>
        /// <param name="idPlayer">Id of the player</param>
        public async Task<ApiResponse> ChangeCapitaine(int idPlayer)
        {
            try
            {
                await _context.Teams.Where(x => x.IdCaptain == idPlayer).Include(x => x.TeamPlayers).ForEachAsync(x => {
                    x.IdCaptain = x.TeamPlayers.FirstOrDefault(c => c.IdPlayer != idPlayer).IdPlayer;
                });
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }


    }
}
