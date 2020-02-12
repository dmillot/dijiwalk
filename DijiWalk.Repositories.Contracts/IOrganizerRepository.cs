//-----------------------------------------------------------------------
// <copyright file="IOrganizerRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Organizer repository
    /// </summary>
    public interface IOrganizerRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to Add</param>
        void Add(Organizer organizer);

        /// <summary>
        /// Definition of the function that will Delete from the database the Organizer passed in the parameters
        /// </summary>
        /// <param name="organizer">Object Organizer to Delete</param>
        Task<ApiResponse> Delete(int idOrganizer);

        /// <summary>
        /// Definition of the method to find an Organizer with his Id
        /// </summary>
        /// <param name="id">The Id of the Organizer</param>
        /// <returns>The Organizer with the Id researched</returns>
        Task<Organizer> Find(int id);

        /// <summary>
        /// Definition of the method to find all Organizer
        /// </summary>
        /// <returns>A List of Organizers</returns>
        Task<IEnumerable<Organizer>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to Update</param>
        void Update(Organizer organizer);
    }
}
