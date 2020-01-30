//-----------------------------------------------------------------------
// <copyright file="ITransportRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Transport repository
    /// </summary>
    public interface ITransportRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object Transport to Add</param>
        void Add(Transport teamRoute);

        /// <summary>
        /// Definition of the function that will Delete from the database the Transport passed in the parameters
        /// </summary>
        /// <param name="teamRoute">Object Transport to Delete</param>
        void Delete(Transport teamRoute);

        /// <summary>
        /// Definition of the method to find an Transport with his Id
        /// </summary>
        /// <param name="id">The Id of the Transport</param>
        /// <returns>The Transport with the Id researched</returns>
        Transport Find(int id);

        /// <summary>
        /// Definition of the method to find all Transport
        /// </summary>
        /// <returns>A List of Transports</returns>
        IEnumerable<Transport> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object Transport to Update</param>
        void Update(Transport teamRoute);
    }
}
