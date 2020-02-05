//-----------------------------------------------------------------------
// <copyright file="MissionRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Mission to the database
    /// </summary>
    public class MissionRepository : IMissionRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public MissionRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">Object Mission to Add</param>
        public void Add(Mission mission)
        {
           _context.Missions.Add(mission);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Mission passed in the parameters
        /// </summary>
        /// <param name="mission">Object Mission to Delete</param>
        public void Delete(Mission mission)
        {
           _context.Missions.Remove(mission);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Mission with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Mission</param>
        /// <returns>The Mission with the Id researched</returns>
        public async Task<Mission> Find(int id)
        {
            return await _context.Missions.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Mission from the database
        /// </summary>
        /// <returns>A List with all Mission</returns>
        public async Task<IEnumerable<Mission>> FindAll()
        {
            return await _context.Missions.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">Object Mission to Update</param>
        public void Update(Mission mission)
        {
           _context.Missions.Update(mission);
           _context.SaveChanges();
        }
    }
}
