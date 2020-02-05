//-----------------------------------------------------------------------
// <copyright file="ITeamAnswerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the TeamAnswer repository
    /// </summary>
    public interface ITeamAnswerRepository
    {
        /// <summary>
        /// Definition of the function that will Add the TeamAnswer passed in the parameters to the database
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Add</param>
        void Add(TeamAnswer teamAnswer);

        /// <summary>
        /// Definition of the function that will Delete from the database the TeamAnswer passed in the parameters
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Delete</param>
        void Delete(TeamAnswer teamAnswer);

        /// <summary>
        /// Definition of the method to find an TeamAnswer with his Id
        /// </summary>
        /// <param name="id">The Id of the TeamAnswer</param>
        /// <returns>The TeamAnswer with the Id researched</returns>
        Task<TeamAnswer> Find(int id);

        /// <summary>
        /// Definition of the method to find all TeamAnswer
        /// </summary>
        /// <returns>A List of TeamAnswer</returns>
        Task<IEnumerable<TeamAnswer>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the TeamAnswer passed in the parameters to the database
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Update</param>
        void Update(TeamAnswer teamAnswer);
    }
}
