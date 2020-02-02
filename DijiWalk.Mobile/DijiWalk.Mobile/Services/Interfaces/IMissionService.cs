using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IMissionService
    {
        /// <summary>
        /// Récupère toutes les missions
        /// </summary>
        /// <returns>Retourne toute la liste des missions (attente)</returns>
        Task<List<Mission>> GetMissions();


        /// <summary>
        /// Récupère une mission spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la mission</param>
        /// <returns>Retourne la mission (attente)</returns>
        Task<Mission> GetMissionById(int id);
        
    }
}