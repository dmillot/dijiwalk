using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITeamAnswerService
    {
        /// <summary>
        /// Récupère toutes les réponses de toutes équipes
        /// </summary>
        /// <returns>Retourne toute la liste des réponses de toutes équipes (attente)</returns>
        Task<List<TeamAnswer>> GetTeamAnswers();

        /// <summary>
        /// Récupère une réponse d'une équipe spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la réponse d'une équipe</param>
        /// <returns>Retourne la réponse d'une équipe (attente)</returns>
        Task<TeamAnswer> GetTeamAnswerById(int id);

    }
}