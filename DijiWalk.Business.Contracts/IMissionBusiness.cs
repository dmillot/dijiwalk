using DijiWalk.Common.Response;
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
        Task<ApiResponse> DeleteAllFromStep(int idStep);
    }
}
