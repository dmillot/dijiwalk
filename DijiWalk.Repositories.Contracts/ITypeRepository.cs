//-----------------------------------------------------------------------
// <copyright file="ITypeRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Type repository
    /// </summary>
    public interface ITypeRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Add</param>
        void Add(Type type);

        /// <summary>
        /// Definition of the function that will Delete from the database the Type passed in the parameters
        /// </summary>
        /// <param name="type">Object Type to Delete</param>
        Task<ApiResponse> Delete(int idType);

        /// <summary>
        /// Definition of the method to find an Type with his Id
        /// </summary>
        /// <param name="id">The Id of the Type</param>
        /// <returns>The Type with the Id researched</returns>
        Task<Type> Find(int id);

        /// <summary>
        /// Definition of the method to find all Type
        /// </summary>
        /// <returns>A List of Types</returns>
        Task<IEnumerable<Type>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Update</param>
        void Update(Type type);
    }
}
