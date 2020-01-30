//-----------------------------------------------------------------------
// <copyright file="IAdministratorRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Administrator repository
    /// </summary>
    public interface IAdministratorRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Administrator passed in the parameters to the database
        /// </summary>
        /// <param name="administrator">Object Administrator to Add</param>
        void Add(Administrator administrator);

        /// <summary>
        /// Definition of the function that will Delete from the database the Administrator passed in the parameters
        /// </summary>
        /// <param name="administrator">Object Administrator to Delete</param>
        void Delete(Administrator administrator);

        /// <summary>
        /// Definition of the method to find an Administrator with his Id
        /// </summary>
        /// <param name="id">The Id of the Administrator</param>
        /// <returns>The Administrator with the Id researched</returns>
        Administrator Find(int id);

        /// <summary>
        /// Definition of the method to find all Administrator
        /// </summary>
        /// <returns>A List of Administrator</returns>
        IEnumerable<Administrator> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Administrator passed in the parameters to the database
        /// </summary>
        /// <param name="administrator">Object Administrator to Update</param>
        void Update(Administrator administrator);
    }
}
