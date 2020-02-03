using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Récupère toutes les games
        /// </summary>
        /// <returns>Retourne toute la liste des games (attente)</returns>
        Task<List<Game>> GetGames();

        /// <summary>
        /// Récupère une game spécifique en fonction de l'ID
        /// </summary>
        /// <param name="id">ID de la game</param>
        /// <returns>Retourne la game (attente)</returns>
        Task<Game> GetGameById(int id);

    }
}
