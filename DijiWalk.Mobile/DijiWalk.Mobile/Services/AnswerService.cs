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
    public class AnswerService : IAnswerService
    {

        /// <summary>
        /// Récupère toutes les réponses
        /// </summary>
        /// <returns>Retourne toute la liste des réponses (attente)</returns>
        public async Task<List<Answer>> GetAnswers()
        {
            return JsonConvert.DeserializeObject<List<Answer>>(await CommonService.GetAll(typeof(Answer)));
        }

        /// <summary>
        /// Récupère une réponse spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la réponse</param>
        /// <returns>Retourne la réponse (attente)</returns>
        public async Task<Answer> GetAnswerById(int id)
        {
            return JsonConvert.DeserializeObject<Answer>(await CommonService.GetId(id, typeof(Answer)));
        }

    }
}

