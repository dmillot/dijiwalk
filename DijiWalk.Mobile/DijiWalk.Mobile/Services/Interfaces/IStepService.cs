using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IStepService
    {
        /// <summary>
        /// Récupère tous les étapes
        /// </summary>
        /// <returns>Retourne toute la liste des étapes (attente)</returns>
        Task<List<Step>> GetSteps();

        /// <summary>
        /// Récupère une étape spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'étape</param>
        /// <returns>Retourne l'étape (attente)</returns>
        Task<Step> GetStepById(int id);
    }
}