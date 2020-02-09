using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IAnswerBusiness
    {
        /// <summary>
        /// Method to Delete all link between question and answer
        /// </summary>
        /// <param name="idTrial">Id of the trial</param>
        Task<ApiResponse> DeleteAllFromTrial(int idTrial);

        /// <summary>
        /// Method to add answer of trial of duplicate mission
        /// </summary>
        /// <param name="answers">List of new answers</param>
        Task<ApiResponse> AddFromNewTrialFromMissionFromStep(List<Answer> answers);
    }
}
