//-----------------------------------------------------------------------
// <copyright file="IAuthentificationRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the authentification repository
    /// </summary>
    public interface IAuthentificationBusiness
    {
        /// <summary>
        /// Definition of the function that will authentificate the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to authentificate</param>
        Task<Player> Authentificate(Player player);

        /// <summary>
        /// Definition of the function that will authentificate the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Organizer to authentificate</param>
        Task<Organizer> Authentificate(Organizer player);

    }
}
