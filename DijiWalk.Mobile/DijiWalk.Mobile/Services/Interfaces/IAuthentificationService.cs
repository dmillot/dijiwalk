using DijiWalk.Entities;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IAuthentificationService
    {

        /// <summary>
        /// Envoi à l'API les informations de connexion en POST
        /// </summary>
        /// <returns>Retourne la réponse de l'API (attente)</returns>
        Task<JWTTokens> Authentificate(Player login);

    }
}