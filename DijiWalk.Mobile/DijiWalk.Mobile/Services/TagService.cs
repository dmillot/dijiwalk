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
    public class TagService : ITagService
    {

        /// <summary>
        /// Récupère tous les tags
        /// </summary>
        /// <returns>Retourne toute la liste des tags (attente)</returns>
        public async Task<List<Tag>> GetTags()
        {
            return JsonConvert.DeserializeObject<List<Tag>>(await CommonService.GetAll(typeof(Tag)));
        }

        /// <summary>
        /// Récupère un tag spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du tag</param>
        /// <returns>Retourne le tag (attente)</returns>
        public async Task<Tag> GetTagById(int id)
        {
            return JsonConvert.DeserializeObject<Tag>(await CommonService.GetId(id, typeof(Tag)));
        }

    }
}

