using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IPlayService
    {
        /// <summary>
        /// Récupère tous les teams des parties
        /// </summary>
        /// <returns>Retourne toute la liste des teams des parties (attente)</returns>
        Task<List<Play>> GePlays();
    }
}