using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface ITeamBusiness
    {
        /// <summary>
        /// Method to check if team as only the player want to delete
        /// </summary>
        /// <param name="idPlayer">Id of the player</param>
        Task<bool> OnlyThisPlayer(int idPlayer);

        /// <summary>
        /// Method to Delete all team player
        /// </summary>
        /// <param name="idTeam">id of the team</param>
        Task<ApiResponse> DeleteAllFromTeam(int idTeam);

        /// <summary>
        /// Method to Delete all team player
        /// </summary>
        /// <param name="idPlayer">id of the player</param>
        Task<ApiResponse> DeleteAllFromPlayer(int idPlayer);

        /// <summary>
        /// Method to Delete all team route with route id
        /// </summary>
        /// <param name="idRoute">id of the route</param>
        Task<ApiResponse> DeleteTeamRoutesFromRoute(int idRoute);


        /// <summary>
        /// Method to check if team has participed to a game
        /// </summary>
        /// <param name="idTeam">id of a team</param>
        Task<bool> ContainsTeams(int idTeam);


        /// <summary>
        /// Method to check if player has participed to a game
        /// </summary>
        /// <param name="idPlayer">id of a player</param>
        Task<bool> ContainsTeamsWithPlayer(int idPlayer);

        /// <summary>
        /// Method to separate team and mission 
        /// </summary>
        /// <param name="team">Object List team</param>
        Team SeparateTeam(Team team);

        /// <summary>
        /// Method to check if a player is in two or more teams
        /// </summary>
        /// <param name="plays">List of plays</param>
        /// <returns>false if not duplicate, true if duplicate</returns>
        Task<bool> DuplicatePlayer(List<Play> plays);

    }
}
