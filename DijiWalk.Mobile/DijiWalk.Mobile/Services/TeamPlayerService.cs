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
    public class TeamPlayerService : ITeamPlayerService
    {

        /// <summary>
        /// Récupère toutes les équipes de tous les participants
        /// </summary>
        /// <returns>Retourne toute la liste des participants de toutes les équipes (attente)</returns>
        public async Task<List<TeamPlayer>> GetTeamPlayers()
        {
            return JsonConvert.DeserializeObject<List<TeamPlayer>>(await CommonService.GetAll(typeof(TeamPlayer)));
        }

    }
}

