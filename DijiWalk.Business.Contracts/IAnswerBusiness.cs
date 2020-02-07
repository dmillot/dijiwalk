using DijiWalk.Common.Response;
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
    }
}
