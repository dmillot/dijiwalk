using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IOrganizerService
    {
        /// <summary>
        /// Récupère tous les organisateurs
        /// </summary>
        /// <returns>Retourne toute la liste des organisateurs (attente)</returns>
        Task<List<Organizer>> GetOrganisateurs();

        /// <summary>
        /// Récupère un organisateur spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'organisateur</param>
        /// <returns>Retourne l'organisateur (attente)</returns>
        Task<Organizer> GetOrganisateurById(int id);
    }
}