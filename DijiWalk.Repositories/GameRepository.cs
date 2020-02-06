//-----------------------------------------------------------------------
// <copyright file="GameRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public GameRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Add</param>
        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Game passed in the parameters
        /// </summary>
        /// <param name="game">Object Game to Delete</param>
        public void Delete(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
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
