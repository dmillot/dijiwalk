using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface ITeamPlayerBusiness
    {
        /// <summary>
        /// Method to Add a list of TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayers">List of object TeamPlayer to Add</param>
        Task<ApiResponse> AddRange(List<TeamPlayer> teamPlayers);

        /// <summary>
        /// Method to set up list of TeamPlayer to add to a game
        /// </summary>
        /// <param name="teamPlayer">Object list of TeamPlayer to set up</param>
        /// <param name="idTeam">Id of the team</param>
        Task<ApiResponse> SetUp(List<TeamPlayer> teamPlayer, int idTeam);

        /// <summary>
        /// Method to Delete all TeamPlayer
        /// </summary>
        /// <param name="teamPlayer">List of TeamPlayer to delete</param>
        Task<ApiResponse> DeleteFromTeam(List<TeamPlayer> teamPlayer);
    }
}
