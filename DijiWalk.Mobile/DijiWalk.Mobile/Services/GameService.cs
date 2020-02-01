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
    public class GameService : IGameService
    {

        /// <summary>
        /// Récupère toutes les games
        /// </summary>
        /// <returns>Retourne toute la liste des games (attente)</returns>
        public async Task<List<Game>> GetGames()
        {
            return JsonConvert.DeserializeObject<List<Game>>(await CommonService.GetAll(typeof(Game)));
        }

        /// <summary>
        /// Récupère une game spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la game</param>
        /// <returns>Retourne la game (attente)</returns>
        public async Task<Game> GetGameById(int id)
        {
            return JsonConvert.DeserializeObject<Game>(await CommonService.GetId(id, typeof(Game)));
        }

    }
}

