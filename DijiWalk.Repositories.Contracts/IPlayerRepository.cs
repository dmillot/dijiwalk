//-----------------------------------------------------------------------
// <copyright file="IPlayerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Player repository
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to Add</param>
        Task<ApiResponse> Add(Player player);
        
        /// <summary>
        /// Definition of the function that will Delete from the database the Player passed in the parameters
        /// </summary>
        /// <param name="player">Object Player to Delete</param>
        Task<ApiResponse> Delete(int idPlayer);

        /// <summary>
        /// Definition of the method to find an Player with his Id
        /// </summary>
        /// <param name="id">The Id of the Player</param>
        /// <returns>The Player with the Id researched</returns>
        Task<Player> Find(int id);

        /// <summary>
        /// Definition of the method to find all Player
        /// </summary>
        /// <returns>A List of Players</returns>
        Task<IEnumerable<Player>> FindAll();

        /// <summary>
        /// Method that will Update the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to Update</param>
        Task<ApiResponse> Update(Player player);

        /// <summary>
        /// Method to get all previous games of a player.
        /// </summary>
        /// <param name="player">Object player</param>
        /// <returns>List of previous games</returns>
        Task<IEnumerable<Game>> GetPreviousGames(Player player);
    }
}
