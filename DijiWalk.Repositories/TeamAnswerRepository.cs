//-----------------------------------------------------------------------
// <copyright file="TeamAnswerRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object TeamAnswer to the database
    /// </summary>
    public class TeamAnswerRepository : ITeamAnswerRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamAnswerRepository(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add the TeamAnswer passed in the parameters to the database
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Add</param>
        public void Add(TeamAnswer teamAnswer)
        {
            _context.Teamanswers.Add(teamAnswer);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamAnswer passed in the parameters
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Delete</param>
        public void Delete(TeamAnswer teamAnswer)
        {
            _context.Teamanswers.Remove(teamAnswer);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an TeamAnswer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the TeamAnswer</param>
        /// <returns>The TeamAnswer with the Id researched</returns>
        public async Task<TeamAnswer> Find(int id)
        {
            return await _context.Teamanswers.FindAsync(id);
        }

        /// <summary>
        /// Method to find all TeamAnswer from the database
        /// </summary>
        /// <returns>A List with all TeamAnswers</returns>
        public async Task<IEnumerable<TeamAnswer>> FindAll()
        {
            return await _context.Teamanswers.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the TeamAnswer passed in the parameters to the database
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Update</param>
        public void Update(TeamAnswer teamAnswer)
        {
            _context.Teamanswers.Update(teamAnswer);
            _context.SaveChanges();
        }
    }
}
