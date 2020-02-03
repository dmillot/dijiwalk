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
    public class RouteTagService : IRouteTagService
    {

        /// <summary>
        /// Récupère tous les tags pour toutes les parties
        /// </summary>
        /// <returns>Retourne toute la liste des tags pour toutes les parties (attente)</returns>
        public async Task<List<RouteTag>> GetRouteTags()
        {
            return JsonConvert.DeserializeObject<List<RouteTag>>(await CommonService.GetAll(typeof(RouteTag)));
        }

       
    }
}

