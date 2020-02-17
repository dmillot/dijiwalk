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
    public class TeamPlayerBusiness : ITeamPlayerBusiness
    {
        private readonly SmartCityContext _context;

        public TeamPlayerBusiness(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add a list of TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayers">List of object TeamPlayer to Add</param>
        public async Task<ApiResponse> AddRange(List<TeamPlayer> teamPlayers)
        {
            try
            {
                await _context.Teamplayers.AddRangeAsync(teamPlayers);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = teamPlayers };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to set up list of TeamPlayer to add to a game
        /// </summary>
        /// <param name="teamPlayer">Object list of TeamPlayer to set up</param>
        /// <param name="idTeam">Id of the team</param>
        public async Task<ApiResponse> SetUp(List<TeamPlayer> teamPlayer, int idTeam)
        {
            return await this.AddRange(teamPlayer.Select(tp => { tp.IdTeam = idTeam; return tp; }).ToList());
        }

        /// <summary>
        /// Method to Delete all TeamPlayer
        /// </summary>
        /// <param name="teamPlayer">List of TeamPlayer to delete</param>
        public async Task<ApiResponse> DeleteFromTeam(List<TeamPlayer> teamPlayer)
        {
            try
            {
                _context.Teamplayers.RemoveRange(teamPlayer);
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
