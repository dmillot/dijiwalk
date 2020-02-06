//-----------------------------------------------------------------------
// <copyright file="ITransportRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Transport repository
    /// </summary>
    public interface ITransportRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Add</param>
        void Add(Transport transport);

        /// <summary>
        /// Definition of the function that will Delete from the database the Transport passed in the parameters
        /// </summary>
        /// <param name="idTransport">Id Transport to Delete</param>
        Task<ApiResponse> Delete(int idTransport);

        /// <summary>
        /// Definition of the method to find an Transport with his Id
        /// </summary>
        /// <param name="id">The Id of the Transport</param>
        /// <returns>The Transport with the Id researched</returns>
        Task<Transport> Find(int id);

        /// <summary>
        /// Definition of the method to find all Transport
        /// </summary>
        /// <returns>A List of Transports</returns>
        Task<IEnumerable<Transport>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Update</param>
        void Update(Transport transport);
    }
}
