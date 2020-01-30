﻿//-----------------------------------------------------------------------
// <copyright file="IGameRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories.Contracts
{
    using System.Collections.Generic;
    using DijiWalk.Entities;

    /// <summary>
    /// This is the interface for the Game repository
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Definition of the function that will Add the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Add</param>
        void Add(Game game);

        /// <summary>
        /// Definition of the function that will Delete from the database the Game passed in the parameters
        /// </summary>
        /// <param name="game">Object Game to Delete</param>
        void Delete(Game game);

        /// <summary>
        /// Definition of the method to find an Game with his Id
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The Game with the Id researched</returns>
        Game Find(int id);

        /// <summary>
        /// Definition of the method to find all Game
        /// </summary>
        /// <returns>A List of Games</returns>
        IEnumerable<Game> FindAll();

        /// <summary>
        /// Definition of the function that will Update the Game passed in the parameters to the database
        /// </summary>
        /// <param name="game">Object Game to Update</param>
        void Update(Game game);
    }
}