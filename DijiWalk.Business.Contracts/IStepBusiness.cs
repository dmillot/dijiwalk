using DijiWalk.Common.Response;
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
    }
}
