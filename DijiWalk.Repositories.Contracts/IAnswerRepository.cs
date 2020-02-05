//-----------------------------------------------------------------------
// <copyright file="IAnswerRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Answer repository
    /// </summary>
    public interface IAnswerRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Answer passed in the parameters to the database
        /// </summary>
        /// <param name="answer">Object Answer to Add</param>
        void Add(Answer answer);

        /// <summary>
        /// Definition of the function that will Delete from the database the Answer passed in the parameters
        /// </summary>
        /// <param name="answer">Object Answer to Delete</param>
        Task<ApiResponse> Delete(int idAnswer);

        /// <summary>
        /// Definition of the method to find an Answer with his Id
        /// </summary>
        /// <param name="id">The Id of the Answer</param>
        /// <returns>The Answer with the Id researched</returns>
        Task<Answer> Find(int id);

        /// <summary>
        /// Definition of the method to find all Answers
        /// </summary>
        /// <returns>A List of Answers</returns>
        Task<IEnumerable<Answer>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Answer passed in the parameters to the database
        /// </summary>
        /// <param name="answer">Object Answer to Update</param>
        void Update(Answer answer);
    }
}
