//-----------------------------------------------------------------------
// <copyright file="ITagRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Tag repository
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// Method to Add the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Add</param>
        Task<ApiResponse> Add(Tag tag);

        /// <summary>
        /// Definition of the function that will Delete from the database the Tag passed in the parameters
        /// </summary>
        /// <param name="tag">Object Tag to Delete</param>
        Task<ApiResponse> Delete(int idTag);

        /// <summary>
        /// Definition of the method to find an Tag with his Id
        /// </summary>
        /// <param name="id">The Id of the Tag</param>
        /// <returns>The Tag with the Id researched</returns>
        Task<Tag> Find(int id);

        /// <summary>
        /// Definition of the method to find all Tag
        /// </summary>
        /// <returns>A List of Tags</returns>
        Task<IEnumerable<Tag>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Update</param>
        void Update(Tag tag);
    }
}
