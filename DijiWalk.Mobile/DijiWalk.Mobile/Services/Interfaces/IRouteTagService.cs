using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IRouteTagService
    {
        /// <summary>
        /// Récupère tous les tags pour toutes les parties
        /// </summary>
        /// <returns>Retourne toute la liste des tags pour toutes les parties (attente)</returns>
        Task<List<RouteTag>> GetRouteTags();
    }
}