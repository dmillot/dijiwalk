//-----------------------------------------------------------------------
// <copyright file="TrialRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Trial to the database
    /// </summary>
    public class TrialRepository : ITrialRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TrialRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Trial passed in the parameters to the database
        /// </summary>
        /// <param name="trial">Object Trial to Add</param>
        public void Add(Trial trial)
        {
            _context.Trials.Add(trial);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Trial passed in the parameters
        /// </summary>
        /// <param name="trial">Object Trial to Delete</param>
        public void Delete(Trial trial)
        {
            _context.Trials.Remove(trial);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Trial with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Trial</param>
        /// <returns>The Trial with the Id researched</returns>
        public async Task<Trial> Find(int id)
        {
            return await _context.Trials.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Trial from the database
        /// </summary>
        /// <returns>A List with all Trial</returns>
        public async Task<IEnumerable<Trial>> FindAll()
        {
            return await _context.Trials.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Trial passed in the parameters to the database
        /// </summary>
        /// <param name="trial">Object Trial to Update</param>
        public void Update(Trial trial)
        {
            _context.Trials.Update(trial);
            _context.SaveChanges();
        }
    }
}
