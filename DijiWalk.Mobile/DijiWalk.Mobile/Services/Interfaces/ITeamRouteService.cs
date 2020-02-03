using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITeamRouteService
    {
        /// <summary>
        /// Récupère toutes les équipes de tous les parcours
        /// </summary>
        /// <returns>Retourne toute la liste des équipes de tous les parcours (attente)</returns>
        Task<List<TeamRoute>> GetTeamRoutes();

    }
}