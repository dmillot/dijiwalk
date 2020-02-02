using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITrialService
    {
        /// <summary>
        /// Récupère toutes les questions
        /// </summary>
        /// <returns>Retourne toute la liste des questions (attente)</returns>
        Task<List<Trial>> GetTrials();

        /// <summary>
        /// Récupère une question spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la question</param>
        /// <returns>Retourne la question (attente)</returns>
        Task<Trial> GetTrialById(int id);
    }
}