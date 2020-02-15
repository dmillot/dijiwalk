//-----------------------------------------------------------------------
// <copyright file="ITeamRouteRepository.cs" company="DijiWalk">
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
    /// This is the interface for the TeamRoute repository
    /// </summary>
    public interface ITeamRouteRepository
    {
        /// <summary>
        /// Definition of the function that will Add the TeamRoute passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Add</param>
        void Add(TeamRoute teamRoute);

        /// <summary>
        /// Definition of the function that will Delete from the database the TeamRoute passed in the parameters
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Delete</param>
        Task<ApiResponse> Delete(int idTeamRoute);

        /// <summary>
        /// Method to find an TeamRoute with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the game</param>
        /// <returns>The TeamRoute with the Id researched</returns>
        Task<List<TeamRoute>> FindInGame(int id);

        /// <summary>
        /// Definition of the method to find all TeamRoute
        /// </summary>
        /// <returns>A List of TeamRoutes</returns>
        Task<IEnumerable<TeamRoute>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the TeamRoute passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Update</param>
        Task<ApiResponse> Update(TeamRoute teamRoute, bool validate);
    }
}
