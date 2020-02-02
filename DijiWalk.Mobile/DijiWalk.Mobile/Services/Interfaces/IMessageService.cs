using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// Récupère tous les messages
        /// </summary>
        /// <returns>Retourne toute la liste des messages (attente)</returns>
        Task<List<Message>> GetMessages();


        /// <summary>
        /// Récupère un message spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du message</param>
        /// <returns>Retourne le message (attente)</returns>
        Task<Message> GetMessageById(int id);

    }
}