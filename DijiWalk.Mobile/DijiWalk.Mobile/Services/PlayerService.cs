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
    public class PlayerService : IPlayerService
    {

        /// <summary>
        /// Récupère tous les participants
        /// </summary>
        /// <returns>Retourne toute la liste des participants (attente)</returns>
        public async Task<List<Player>> GetPlayers()
        {
            return JsonConvert.DeserializeObject<List<Player>>(await CommonService.GetAll(typeof(Player)));
        }

        /// <summary>
        /// Récupère un participant spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du participant</param>
        /// <returns>Retourne le participant (attente)</returns>
        public async Task<Player> GetPlayerById(int id)
        {
            return JsonConvert.DeserializeObject<Player>(await CommonService.GetId(id, typeof(Player)));
        }

    }
}

