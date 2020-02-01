using DijiWalk.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IAdministratorService
    {
        /// <summary>
        /// Récupère tous les administrateurs
        /// </summary>
        /// <returns>Retourne toute la liste des administrateurs (attente)</returns>
        Task<List<Administrator>> GetAdministrators();

        /// <summary>
        /// Récupère un administrateur spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de l'administrateur</param>
        /// <returns>Retourne l'administrateur (attente)</returns>
        Task<Administrator> GetAdministratorById(int id);

    }
}