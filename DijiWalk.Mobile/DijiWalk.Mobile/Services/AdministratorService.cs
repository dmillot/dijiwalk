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
    public class AdministratorService : IAdministratorService
    {

        /// <summary>
        /// Récupère tous les administrateurs
        /// </summary>
        /// <returns>Retourne toute la liste des administrateurs (attente)</returns>
        public async Task<List<Administrator>> GetAdministrators()
        {
            return JsonConvert.DeserializeObject<List<Administrator>>(await CommonService.GetAll(typeof(Administrator)));
        }

        /// <summary>
        /// Récupère un administrateur spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'administrateur</param>
        /// <returns>Retourne l'administrateur (attente)</returns>
        public async Task<Administrator> GetAdministratorById(int id)
        {
            return JsonConvert.DeserializeObject<Administrator>(await CommonService.GetId(id, typeof(Administrator)));
        }

    }
}

