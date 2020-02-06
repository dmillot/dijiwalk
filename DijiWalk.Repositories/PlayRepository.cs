//-----------------------------------------------------------------------
// <copyright file="PlayRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

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
        public async Task<ApiResponse> Delete(int idPlay)
        {
            try
            {
                _context.Plays.Remove(await _context.Plays.FindAsync(idPlay));
                _context.SaveChanges();
                return new ApiResponse { Status = 1, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an Play with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Play</param>
        /// <returns>The Play with the Id researched</returns>
        public async Task<Play> Find(int id)
        {
            return await _context.Plays.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Play from the database
        /// </summary>
        /// <returns>A List with all Plays</returns>
        public async Task<IEnumerable<Play>> FindAll()
        {
            return await _context.Plays.ToListAsync();
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
