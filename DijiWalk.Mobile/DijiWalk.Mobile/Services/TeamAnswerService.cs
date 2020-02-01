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
    public class TeamAnswerService : ITeamAnswerService
    {

        /// <summary>
        /// Récupère toutes les réponses de toutes équipes
        /// </summary>
        /// <returns>Retourne toute la liste des réponses de toutes équipes (attente)</returns>
        public async Task<List<TeamAnswer>> GetTeamAnswers()
        {
            return JsonConvert.DeserializeObject<List<TeamAnswer>>(await CommonService.GetAll(typeof(TeamAnswer)));
        }

        /// <summary>
        /// Récupère une réponse d'une équipe spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la réponse d'une équipe</param>
        /// <returns>Retourne la réponse d'une équipe (attente)</returns>
        public async Task<TeamAnswer> GetTeamAnswerById(int id)
        {
            return JsonConvert.DeserializeObject<TeamAnswer>(await CommonService.GetId(id, typeof(TeamAnswer)));
        }

    }
}

