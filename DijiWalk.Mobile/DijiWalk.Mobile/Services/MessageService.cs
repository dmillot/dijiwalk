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
    public class MessageService : IMessageService
    {

        /// <summary>
        /// Récupère tous les messages
        /// </summary>
        /// <returns>Retourne toute la liste des messages (attente)</returns>
        public async Task<List<Message>> GetMessages()
        {
            return JsonConvert.DeserializeObject<List<Message>>(await CommonService.GetAll(typeof(Message)));
        }

        /// <summary>
        /// Récupère un message spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du message</param>
        /// <returns>Retourne le message (attente)</returns>
        public async Task<Message> GetMessageById(int id)
        {
            return JsonConvert.DeserializeObject<Message>(await CommonService.GetId(id, typeof(Message)));
        }

    }
}

