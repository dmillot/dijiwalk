//-----------------------------------------------------------------------
// <copyright file="PlayerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Encryption;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class authentification player
    /// </summary>
    public class AuthentificationRepository : IAuthentificationRepository
    {
        private readonly SmartCityContext _context;

        private readonly ICryption _cryption;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public AuthentificationRepository(SmartCityContext context, ICryption cryption)
        {
            _context = context;
            _cryption = cryption;
        }


        /// <summary>
        /// Definition of the function that will authentificate the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to authentificate</param>
        public async Task<Player> Authentificate(Player player)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Login == player.Login && p.Password == _cryption.Encrypt(player.Password));
        }

        /// <summary>
        /// Definition of the function that will authentificate the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to authentificate</param>
        public async Task<Organizer> Authentificate(Organizer organizer)
        {
            return await _context.Organizers.FirstOrDefaultAsync(p => p.Login == organizer.Login && p.Password == _cryption.Encrypt(organizer.Password));
        }

    }
}
