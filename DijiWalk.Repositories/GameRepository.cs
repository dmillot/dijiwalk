//-----------------------------------------------------------------------
// <copyright file="GameRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Game to the database
    /// </summary>
    public class GameRepository : IGameRepository
    {


        private readonly SmartCityContext _context;
        private readonly IApiResponse _apiResponse;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public GameRepository(SmartCityContext context, IApiResponse apiResponse)
        {
            this._context = context;
            this._apiResponse = apiResponse;
        }

        /// <summary>
        /// Method to Add the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Add</param>
        public async Task<Game> Add(Game game)
        {
            try
            {
                _context.Games.Add(game);
                _context.SaveChanges();
                return game;
            }
            catch (Exception e)
            {
                return new Game();
            }
        }

        /// <summary>
        /// Method to Delete from the database the Game passed in the parameters
        /// </summary>
        /// <param name="game">Object Game to Delete</param>
        public async Task<string> Delete(int id)
        {
            try
            {
                _context.Games.Remove(await _context.Games.FindAsync(id));
                _context.SaveChanges();
                return _apiResponse.GetMessageDelete();
            }
            catch (Exception e)
            {
                return _apiResponse.TranslateError(e);
            }
        }

        /// <summary>
        /// Method to check if route is in a game
        /// </summary>
        /// <param name="idRoute">id of a team</param>
        public async Task<bool> ContainsRoute(int idRoute)
        {
            return await _context.Games.AnyAsync(t => t.IdRoute == idRoute);
        }

        /// <summary>
        /// Method to find an Game with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The Game with the Id researched</returns>
        public async Task<Game> Find(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Game from the database
        /// </summary>
        /// <returns>A List with all Games</returns>
        public async Task<IEnumerable<Game>> FindAll()
        {
            return await _context.Games.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Update</param>
        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}
