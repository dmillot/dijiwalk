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
    public class StepService : IStepService
    {

        /// <summary>
        /// Récupère tous les étapes
        /// </summary>
        /// <returns>Retourne toute la liste des étapes (attente)</returns>
        public async Task<List<Step>> GetSteps()
        {
            return JsonConvert.DeserializeObject<List<Step>>(await CommonService.GetAll(typeof(Step)));
        }

        /// <summary>
        /// Récupère une étape spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'étape</param>
        /// <returns>Retourne l'étape (attente)</returns>
        public async Task<Step> GetStepById(int id)
        {
            return JsonConvert.DeserializeObject<Step>(await CommonService.GetId(id, typeof(Step)));
        }

    }
}

