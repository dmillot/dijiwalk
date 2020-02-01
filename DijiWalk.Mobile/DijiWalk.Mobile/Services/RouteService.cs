using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DijiWalk.Common.Exceptions;
using Xamarin.Forms;
using DijiWalk.Mobile.Services.Common;

namespace DijiWalk.Mobile.Services
{
    public class RouteService : IRouteService
    {

        /// <summary>
        /// Récupère tous les parcours
        /// </summary>
        /// <returns>Retourne toute la liste des parcours (attente)</returns>
        public async Task<List<Route>> GetRoutes()
        {
            return JsonConvert.DeserializeObject<List<Route>>(await CommonService.GetAll(typeof(Route)));
        }

        /// <summary>
        /// Récupère un parcours spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du parcours</param>
        /// <returns>Retourne le parcours (attente)</returns>
        public async Task<Route> GetRouteById(int id)
        {
            return JsonConvert.DeserializeObject<Route>(await CommonService.GetId(id, typeof(Route)));
        }

    }
}

