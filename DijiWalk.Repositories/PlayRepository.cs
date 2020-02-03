//-----------------------------------------------------------------------
// <copyright file="PlayRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Play to the database
    /// </summary>
    public class PlayRepository : IPlayRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public PlayRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Play passed in the parameters to the database
        /// </summary>
        /// <param name="play">Object Play to Add</param>
        public void Add(Play play)
        {
           _context.Plays.Add(play);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Play passed in the parameters
        /// </summary>
        /// <param name="play">Object Play to Delete</param>
        public void Delete(Play play)
        {
           _context.Plays.Remove(play);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Play with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Play</param>
        /// <returns>The Play with the Id researched</returns>
        public Play Find(int id)
        {
            return _context.Plays.Find(id);
        }

        /// <summary>
        /// Method to find all Play from the database
        /// </summary>
        /// <returns>A List with all Plays</returns>
        public IEnumerable<Play> FindAll()
        {
            return _context.Plays;
        }

        /// <summary>
        /// Method that will Update the Play passed in the parameters to the database
        /// </summary>
        /// <param name="play">Object Play to Update</param>
        public void Update(Play play)
        {
           _context.Plays.Update(play);
           _context.SaveChanges();
        }
    }
}
