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
    public class TrialService : ITrialService
    {

        /// <summary>
        /// Récupère toutes les questions
        /// </summary>
        /// <returns>Retourne toute la liste des questions (attente)</returns>
        public async Task<List<Trial>> GetTrials()
        {
            return JsonConvert.DeserializeObject<List<Trial>>(await CommonService.GetAll(typeof(Trial)));
        }

        /// <summary>
        /// Récupère une question spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la question</param>
        /// <returns>Retourne la question (attente)</returns>
        public async Task<Trial> GetTrialById(int id)
        {
            return JsonConvert.DeserializeObject<Trial>(await CommonService.GetId(id, typeof(Trial)));
        }

    }
}

