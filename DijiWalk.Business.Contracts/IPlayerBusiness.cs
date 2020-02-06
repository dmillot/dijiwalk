using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IPlayerBusiness
    {

        /// <summary>
        /// Method to change check if login and email are unique
        /// </summary>
        /// <param name="player">Object player</param>
        Task<bool> Check(Player player);

    }
}
