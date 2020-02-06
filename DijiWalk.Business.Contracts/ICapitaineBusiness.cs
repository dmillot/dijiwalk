using DijiWalk.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface ICapitaineBusiness
    {

        /// <summary>
        /// Method to change the capitaine if the player was the capitaine in his team
        /// </summary>
        /// <param name="idPlayer">Id of the player</param>
        Task<ApiResponse> ChangeCapitaine(int idPlayer);

    }
}
