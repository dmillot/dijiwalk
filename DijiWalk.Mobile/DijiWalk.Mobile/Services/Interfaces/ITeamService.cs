using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITeamService
    {
        /// <summary>
        /// Récupère toutes les équipes
        /// </summary>
        /// <returns>Retourne toute la liste des équipes (attente)</returns>
        Task<List<Team>> GetTeams();

        /// <summary>
        /// Récupère une équipe spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'équipe</param>
        /// <returns>Retourne l'équipe (attente)</returns>
        Task<Team> GetTeamById(int id);
    }
}