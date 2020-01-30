//-----------------------------------------------------------------------
// <copyright file="IStepRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Step repository
    /// </summary>
    public interface IStepRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Add</param>
        void Add(Step step);

        /// <summary>
        /// Definition of the function that will Delete from the database the Step passed in the parameters
        /// </summary>
        /// <param name="step">Object Step to Delete</param>
        void Delete(Step step);

        /// <summary>
        /// Definition of the method to find an Step with his Id
        /// </summary>
        /// <param name="id">The Id of the Step</param>
        /// <returns>The Step with the Id researched</returns>
        Step Find(int id);

        /// <summary>
        /// Definition of the method to find all Step
        /// </summary>
        /// <returns>A List of Steps</returns>
        IEnumerable<Step> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Update</param>
        void Update(Step step);
    }
}
