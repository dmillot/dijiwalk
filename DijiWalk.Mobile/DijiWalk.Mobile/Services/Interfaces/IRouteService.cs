using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IRouteService
    {
        /// <summary>
        /// Récupère tous les parcours
        /// </summary>
        /// <returns>Retourne toute la liste des parcours (attente)</returns>
        Task<List<Route>> GetRoutes();

        /// <summary>
        /// Récupère un parcours spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du parcours</param>
        /// <returns>Retourne le parcours (attente)</returns>
        Task<Route> GetRouteById(int id);
    }
}