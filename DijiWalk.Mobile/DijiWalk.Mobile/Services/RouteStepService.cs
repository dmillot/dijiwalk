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
    public class RouteStepService : IRouteStepService
    {

        /// <summary>
        /// Récupère tous les étapes pour chaques parties avec l'ordre
        /// </summary>
        /// <returns>Retourne toute la liste des étapes pour chaques parties avec l'ordre (attente)</returns>
        public async Task<List<RouteStep>> GetRouteSteps()
        {
            return JsonConvert.DeserializeObject<List<RouteStep>>(await CommonService.GetAll(typeof(RouteStep)));
        }

       
    }
}

