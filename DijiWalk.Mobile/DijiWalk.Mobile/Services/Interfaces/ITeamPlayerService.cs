using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITeamPlayerService
    {

        /// <summary>
        /// Récupère toutes les équipes de tous les participants
        /// </summary>
        /// <returns>Retourne toute la liste des participants de toutes les équipes (attente)</returns>
        Task<List<TeamPlayer>> GetTeamPlayers();
    }
}