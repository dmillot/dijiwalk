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
    public class TransportService : ITransportService
    {

        /// <summary>
        /// Récupère tous les modes de transport
        /// </summary>
        /// <returns>Retourne toute la liste des modes de transport (attente)</returns>
        public async Task<List<Transport>> GetTransports()
        {
            return JsonConvert.DeserializeObject<List<Transport>>(await CommonService.GetAll(typeof(Transport)));
        }

        /// <summary>
        /// Récupère un mode de transport spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du mode de transport</param>
        /// <returns>Retourne le mode de transport (attente)</returns>
        public async Task<Transport> GetTransportById(int id)
        {
            return JsonConvert.DeserializeObject<Transport>(await CommonService.GetId(id, typeof(Transport)));
        }

    }
}

