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
    public class TeamRouteService : ITeamRouteService
    {

        /// <summary>
        /// Récupère toutes les équipes de tous les parcours
        /// </summary>
        /// <returns>Retourne toute la liste des équipes de tous les parcours (attente)</returns>
        public async Task<List<TeamRoute>> GetTeamRoutes()
        {
            return JsonConvert.DeserializeObject<List<TeamRoute>>(await CommonService.GetAll(typeof(TeamRoute)));
        }

    }
}

