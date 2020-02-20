//-----------------------------------------------------------------------
// <copyright file="IStepRepository.cs" company="DijiWalk">
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
    /// This is the interface for the Step repository
    /// </summary>
    public interface IStepRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Add</param>
        Task<ApiResponse> Add(Step step);

        /// <summary>
        /// Definition of the function that will Delete from the database the Step passed in the parameters
        /// </summary>
        /// <param name="id">Id of the object Step to Delete</param>
        Task<ApiResponse> Delete(int id);

        /// <summary>
        /// Definition of the method to find an Step with his Id
        /// </summary>
        /// <param name="id">The Id of the Step</param>
        /// <returns>The Step with the Id researched</returns>
        Task<Step> Find(int id);

        /// <summary>
        /// Definition of the method to find all Step
        /// </summary>
        /// <returns>A List of Steps</returns>
        Task<IEnumerable<Step>> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Update</param>
        Task<ApiResponse> Update(Step step);

        /// <summary>
        /// Method to upload on the google cloud storage
        /// </summary>
        /// <param name="image64">Image convert string 64</param>
        /// <param name="fileName">Name of file</param>
        /// <returns></returns>
        Task<ApiResponse> Validate(Validate validate);

        Task<ApiResponse> Check(Validate validate);

        Task<ApiResponse> Test(string url);
    }
}
