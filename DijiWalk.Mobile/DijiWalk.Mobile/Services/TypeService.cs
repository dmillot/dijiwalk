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
    public class TypeService : ITypeService
    {

        /// <summary>
        /// Récupère toutes les types des questions
        /// </summary>
        /// <returns>Retourne toute la liste des types des questions (attente)</returns>
        public async Task<List<Entities.Type>> GetTypes()
        {
            return JsonConvert.DeserializeObject<List<Entities.Type>>(await CommonService.GetAll(typeof(Entities.Type)));
        }

        /// <summary>
        /// Récupère un type de question spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du type de question</param>
        /// <returns>Retourne le type de question (attente)</returns>
        public async Task<Entities.Type> GetTypeById(int id)
        {
            return JsonConvert.DeserializeObject<Entities.Type>(await CommonService.GetId(id, typeof(Entities.Type)));
        }

    }
}

