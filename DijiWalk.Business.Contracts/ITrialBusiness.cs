using DijiWalk.Common.Response;
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
    }
}
