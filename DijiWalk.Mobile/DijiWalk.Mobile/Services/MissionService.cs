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
    public class MissionService : IMissionService
    {

        /// <summary>
        /// Récupère toutes les missions
        /// </summary>
        /// <returns>Retourne toute la liste des missions (attente)</returns>
        public async Task<List<Mission>> GetMissions()
        {
            return JsonConvert.DeserializeObject<List<Mission>>(await CommonService.GetAll(typeof(Mission)));
        }

        /// <summary>
        /// Récupère une mission spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la mission</param>
        /// <returns>Retourne la mission (attente)</returns>
        public async Task<Mission> GetMissionById(int id)
        {
            return JsonConvert.DeserializeObject<Mission>(await CommonService.GetId(id, typeof(Mission)));
        }

    }
}

