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
    public class TeamService : ITeamService
    {

        /// <summary>
        /// Récupère toutes les équipes
        /// </summary>
        /// <returns>Retourne toute la liste des équipes (attente)</returns>
        public async Task<List<Team>> GetTeams()
        {
            return JsonConvert.DeserializeObject<List<Team>>(await CommonService.GetAll(typeof(Team)));
        }

        /// <summary>
        /// Récupère une équipe spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'équipe</param>
        /// <returns>Retourne l'équipe (attente)</returns>
        public async Task<Team> GetTeamById(int id)
        {
            return JsonConvert.DeserializeObject<Team>(await CommonService.GetId(id, typeof(Team)));
        }

    }
}

