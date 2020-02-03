using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface ITypeService
    {
        /// <summary>
        /// Récupère toutes les types des questions
        /// </summary>
        /// <returns>Retourne toute la liste des types des questions (attente)</returns>
        Task<List<Entities.Type>> GetTypes();

        /// <summary>
        /// Récupère un type de question spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID du type de question</param>
        /// <returns>Retourne le type de question (attente)</returns>
        Task<Entities.Type> GetTypeById(int id);
    }
}