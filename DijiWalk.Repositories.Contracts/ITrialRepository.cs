//-----------------------------------------------------------------------
// <copyright file="ITrialRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Trial repository
    /// </summary>
    public interface ITrialRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Trial passed in the parameters to the database
        /// </summary>
        /// <param name="trial">Object Trial to Add</param>
        void Add(Trial trial);

        /// <summary>
        /// Definition of the function that will Delete from the database the Trial passed in the parameters
        /// </summary>
        /// <param name="trial">Object Trial to Delete</param>
        void Delete(Trial trial);

        /// <summary>
        /// Definition of the method to find an Trial with his Id
        /// </summary>
        /// <param name="id">The Id of the Trial</param>
        /// <returns>The Trial with the Id researched</returns>
        Trial Find(int id);

        /// <summary>
        /// Definition of the method to find all Trial
        /// </summary>
        /// <returns>A List of Trials</returns>
        IEnumerable<Trial> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Trial passed in the parameters to the database
        /// </summary>
        /// <param name="trial">Object Trial to Update</param>
        void Update(Trial trial);
    }
}
