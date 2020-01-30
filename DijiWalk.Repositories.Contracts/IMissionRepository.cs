//-----------------------------------------------------------------------
// <copyright file="IMissionRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Mission repository
    /// </summary>
    public interface IMissionRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">Object Mission to Add</param>
        void Add(Mission mission);

        /// <summary>
        /// Definition of the function that will Delete from the database the Mission passed in the parameters
        /// </summary>
        /// <param name="mission">Object Mission to Delete</param>
        void Delete(Mission mission);

        /// <summary>
        /// Definition of the method to find an Mission with his Id
        /// </summary>
        /// <param name="id">The Id of the Mission</param>
        /// <returns>The Mission with the Id researched</returns>
        Mission Find(int id);

        /// <summary>
        /// Definition of the method to find all Mission
        /// </summary>
        /// <returns>A List of Missions</returns>
        IEnumerable<Mission> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">Object Mission to Update</param>
        void Update(Mission mission);
    }
}
