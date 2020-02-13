using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IStepTagBusiness
    {


        /// <summary>
        /// Method to Add a list of StepTag passed in the parameters to the database
        /// </summary>
        /// <param name="stepTags">List of object StepTag to Add</param>
        Task<ApiResponse> AddRange(List<StepTag> stepTags);




        /// <summary>
        /// Method to Delete all step tag 
        /// </summary>
        /// <param name="stepTags">List of step tags to delete</param>
        Task<ApiResponse> DeleteFromStep(List<StepTag> stepTags);

    }
}
