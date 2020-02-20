using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IStepBusiness
    {
        /// <summary>
        /// Method to check if a step is already in game
        /// </summary>
        /// <param name="idStep">id of a step</param>
        Task<bool> ContainsStep(int idStep);

        /// <summary>
        /// Method to separate step and mission 
        /// </summary>
        /// <param name="step">Object step</param>
        Step SeparateStep(Step step);

        /// <summary>
        /// Method to get validation of step 
        /// </summary>
        /// <param name="idStep">id of the step to compare</param>
        Task<Step> GetAnalyzeStep(int idStep);
    }
}
