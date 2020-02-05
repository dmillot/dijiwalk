﻿//-----------------------------------------------------------------------
// <copyright file="PlayerRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Player to the database
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public PlayerRepository(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to Add</param>
        public void Add(Player player)
        {
           _context.Players.Add(player);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Player passed in the parameters
        /// </summary>
        /// <param name="player">Object Player to Delete</param>
        public void Delete(Player player)
        {
           _context.Players.Remove(player);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Player with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Player</param>
        /// <returns>The Player with the Id researched</returns>
        public async Task<Player> Find(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Player from the database
        /// </summary>
        /// <returns>A List with all Players</returns>
        public async Task<IEnumerable<Player>> FindAll()
        {
            return await _context.Players.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Player passed in the parameters to the database
        /// </summary>
        /// <param name="player">Object Player to Update</param>
        public void Update(Player player)
        {
           _context.Players.Update(player);
           _context.SaveChanges();
        }
    }
}
