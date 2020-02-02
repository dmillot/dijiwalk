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
    public class OrganizerService : IOrganizerService
    {

        /// <summary>
        /// Récupère tous les organisateurs
        /// </summary>
        /// <returns>Retourne toute la liste des organisateurs (attente)</returns>
        public async Task<List<Organizer>> GetOrganisateurs()
        {
            return JsonConvert.DeserializeObject<List<Organizer>>(await CommonService.GetAll(typeof(Organizer)));
        }

        /// <summary>
        /// Récupère un organisateur spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'organisateur</param>
        /// <returns>Retourne l'organisateur (attente)</returns>
        public async Task<Organizer> GetOrganisateurById(int id)
        {
            return JsonConvert.DeserializeObject<Organizer>(await CommonService.GetId(id, typeof(Organizer)));
        }

    }
}

