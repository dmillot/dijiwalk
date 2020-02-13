using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IStepAnalyseBusiness
    {


        /// <summary>
        /// Method to Add a list of StepTag passed in the parameters to the database
        /// </summary>
        /// <param name="stepTags">List of object StepTag to Add</param>
        /// <param name="stepValidations">List of object StepValidation to Add</param>
        Task<ApiResponse> AddRange(List<StepTag> stepTags, List<StepValidation> stepValidations);




        /// <summary>
        /// Method to Delete all step analyze
        /// </summary>
        /// <param name="idStep">Id of step</param>
        Task<ApiResponse> DeleteFromStep(int idStep);

    }
}
