using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IRouteStepService
    {
        /// <summary>
        /// Récupère tous les étapes pour chaques parties avec l'ordre
        /// </summary>
        /// <returns>Retourne toute la liste des étapes pour chaques parties avec l'ordre (attente)</returns>
        Task<List<RouteStep>> GetRouteSteps();
    }
}