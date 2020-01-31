//-----------------------------------------------------------------------
// <copyright file="TeamPlayerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;

    /// <summary>
    /// Class that connect the Object TeamPlayer to the database
    /// </summary>
    public class TeamPlayerRepository : ITeamPlayerRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamPlayerRepository(SmartCityContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to Add the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Add</param>
        public void Add(TeamPlayer teamPlayer)
        {
           _context.Teamplayers.Add(teamPlayer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamPlayer passed in the parameters
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Delete</param>
        public void Delete(TeamPlayer teamPlayer)
        {
           _context.Teamplayers.Remove(teamPlayer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an TeamPlayer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the TeamPlayer</param>
        /// <returns>The TeamPlayer with the Id researched</returns>
        public TeamPlayer Find(int id)
        {
            return _context.Teamplayers.Find(id);
        }

        /// <summary>
        /// Method to find all TeamPlayer from the database
        /// </summary>
        /// <returns>A List with all TeamPlayers</returns>
        public IEnumerable<TeamPlayer> FindAll()
        {
            return _context.Teamplayers;
        }

        /// <summary>
        /// Method that will Update the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Update</param>
        public void Update(TeamPlayer teamPlayer)
        {
           _context.Teamplayers.Update(teamPlayer);
           _context.SaveChanges();
        }
    }
}
