//-----------------------------------------------------------------------
// <copyright file="IPlayRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Play repository
    /// </summary>
    public interface IPlayRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Play passed in the parameters to the database
        /// </summary>
        /// <param name="play">Object Play to Add</param>
        Task<ApiResponse> Add(Play play);

        /// <summary>
        /// Definition of the function that will Delete from the database the Play passed in the parameters
        /// </summary>
        /// <param name="play">Object Play to Delete</param>
        Task<ApiResponse> Delete(int idGame, int idTeam);

        /// <summary>
        /// Definition of the method to find an Play with his Id
        /// </summary>
        /// <param name="id">The Id of the Play</param>
        /// <returns>The Play with the Id researched</returns>
        Task<Play> Find(int id);

        /// <summary>
        /// Definition of the method to find all Play
        /// </summary>
        /// <returns>A List of Plays</returns>
        Task<IEnumerable<Play>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Play passed in the parameters to the database
        /// </summary>
        /// <param name="play">Object Play to Update</param>
        void Update(Play play);
    }
}
