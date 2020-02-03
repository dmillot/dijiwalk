using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IAnswerService
    {
        /// <summary>
        /// Récupère toutes les réponses
        /// </summary>
        /// <returns>Retourne toute la liste des réponses (attente)</returns>
        Task<List<Answer>> GetAnswers();


        /// <summary>
        /// Récupère une réponse spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la réponse</param>
        /// <returns>Retourne la réponse (attente)</returns>
        Task<Answer> GetAnswerById(int id);
    }
}