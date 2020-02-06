//-----------------------------------------------------------------------
// <copyright file="ITeamPlayerRepository.cs" company="DijiWalk">
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
    /// This is the interface for the TeamPlayer repository
    /// </summary>
    public interface ITeamPlayerRepository
    {
        /// <summary>
        /// Definition of the function that will Add the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Add</param>
        void Add(TeamPlayer teamPlayer);

        /// <summary>
        /// Definition of the function that will Delete from the database the TeamPlayer passed in the parameters
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Delete</param>
        Task<ApiResponse> Delete(int idTeamPlayer);

        /// <summary>
        /// Definition of the method to find an TeamPlayer with his Id
        /// </summary>
        /// <param name="id">The Id of the TeamPlayer</param>
        /// <returns>The TeamPlayer with the Id researched</returns>
        Task<TeamPlayer> Find(int id);

        /// <summary>
        /// Definition of the method to find all TeamPlayer
        /// </summary>
        /// <returns>A List of TeamPlayer</returns>
        Task<IEnumerable<TeamPlayer>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Update</param>
        void Update(TeamPlayer teamPlayer);
    }
}
