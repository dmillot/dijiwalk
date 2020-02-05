using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Common;
using DijiWalk.Mobile.Services.Interfaces;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DijiWalk.Mobile.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        /// <summary>
        /// Envoi à l'API les informations de connexion en POST
        /// </summary>
        /// <returns>Retourne la réponse de l'API (attente)</returns>
        public async Task<Player> Authentificate(Player login)
        {
            return JsonConvert.DeserializeObject<Player>(await CommonService.Post(String.Concat(Application.Current.Properties["url"], "token"), login));
        }
    }
}
