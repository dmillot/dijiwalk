using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IMissionBusiness
    {
        /// <summary>
        /// Method to Delete all link between step and mission
        /// </summary>
        /// <param name="idStep">Id of the step</param>
        Task<ApiResponse> DeleteAllFromStep(Step step);

        /// <summary>
        /// Method to Delete mission not anymore in the step
        /// </summary>
        /// <param name="missionToDelete">List of mission to delete</param>
        Task<ApiResponse> DeleteNotFromStep(List<Mission> missionToDelete);

        /// <summary>
        /// Method to Add a list of Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">List of object Mission to Add</param>
        Task<ApiResponse> AddRange(List<Mission> missions);

        /// <summary>
        /// Method to separate mission and trials 
        /// </summary>
        /// <param name="missions">Object List mission</param>
        List<Mission> SeparateTrial(List<Mission> missions);

        /// <summary>
        /// Method to set up list of mission to add to a step
        /// </summary>
        /// <param name="mission">Object list of missions to set up</param>
        /// <param name="idStep">Id of the step</param>
        Task<ApiResponse> SetUp(List<Mission> missions, int idStep);
    }
}
