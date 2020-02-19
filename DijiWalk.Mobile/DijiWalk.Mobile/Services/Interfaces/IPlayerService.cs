using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IPlayerService
    {
        /// <summary>
        /// Récupère tous les participants
        /// </summary>
        /// <returns>Retourne toute la liste des participants (attente)</returns>
        Task<List<Player>> GetPlayers();

        /// <summary>
        /// Récupère un participant spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du participant</param>
        /// <returns>Retourne le participant (attente)</returns>
        Task<Player> GetPlayerById(int id);

        /// <summary>
        /// Récupère la liste des anciennes games d'un joueur.
        /// </summary>
        /// <param name="id">L'id du joueur</param>
        /// <returns>La liste des anciennes games</returns>
        Task<List<Game>> GetPreviousGames(int id);

        Task<Game> GetActualGame(int playerId);

        Task<Step> GetCurrentStep(int playerId);
    }
}