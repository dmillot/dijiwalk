//-----------------------------------------------------------------------
// <copyright file="IStepTagRepository.cs" company="DijiWalk">
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
    /// This is the interface for the StepTag repository
    /// </summary>
    public interface IStepTagRepository
    {
        /// <summary>
        /// Method to Add the stepTag passed in the parameters to the database
        /// </summary>
        /// <param name="stepTag">Object stepTag to Add</param>
        void Add(StepTag stepTag);


        /// <summary>
        /// Method to Delete from the database the idStepTag passed in the parameters
        /// </summary>
        /// <param name="idStepTag">Object idStepTag to Delete</param>
        Task<ApiResponse> Delete(int idStepTag);


        /// <summary>
        /// Method to find an StepTag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the StepTag</param>
        /// <returns>The StepTag with the Id researched</returns>
        Task<StepTag> Find(int id);


        /// <summary>
        /// Method to find all StepTag from the database
        /// </summary>
        /// <returns>A List with all StepTag</returns>
        Task<IEnumerable<StepTag>> FindAll();


        /// <summary>
        /// Method that will Update the StepTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object StepTag to Update</param>
        void Update(StepTag stepTag);
    }
}
