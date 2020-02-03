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
    public class PlayService : IPlayService
    {

        /// <summary>
        /// Récupère tous les teams des parties
        /// </summary>
        /// <returns>Retourne toute la liste des teams des parties (attente)</returns>
        public async Task<List<Play>> GePlays()
        {
            return JsonConvert.DeserializeObject<List<Play>>(await CommonService.GetAll(typeof(Play)));
        }

    }
}

