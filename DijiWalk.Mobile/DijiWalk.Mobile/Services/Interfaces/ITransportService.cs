using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITransportService
    {
        /// <summary>
        /// Récupère tous les modes de transport
        /// </summary>
        /// <returns>Retourne toute la liste des modes de transport (attente)</returns>
        Task<List<Transport>> GetTransports();

        /// <summary>
        /// Récupère un mode de transport spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du mode de transport</param>
        /// <returns>Retourne le mode de transport (attente)</returns>
        Task<Transport> GetTransportById(int id);
    }
}