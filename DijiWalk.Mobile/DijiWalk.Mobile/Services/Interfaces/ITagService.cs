using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITagService
    {
        /// <summary>
        /// Récupère tous les tags
        /// </summary>
        /// <returns>Retourne toute la liste des tags (attente)</returns>
        Task<List<Tag>> GetTags();

        /// <summary>
        /// Récupère un tag spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du tag</param>
        /// <returns>Retourne le tag (attente)</returns>
        Task<Tag> GetTagById(int id);
    }
}