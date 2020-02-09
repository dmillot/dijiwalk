using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface ITrialBusiness
    {
        /// <summary>
        /// Method to Delete all link between mission and question
        /// </summary>
        /// <param name="idMission">Id of the mission</param>
        Task<ApiResponse> DeleteAllFromMission(int idMission);

        /// <summary>
        /// Method to add trial of duplicate mission
        /// </summary>
        /// <param name="trials">List of trials</param>
        Task<ApiResponse> AddFromNewMissionFromStep(List<Trial> trials);

        /// <summary>
        /// Method to add answer of trial of duplicate mission
        /// </summary>
        /// <param name="trials">List of new answers</param>
        List<Trial> SeparateAnswer(List<Trial> trials);
    }
}
