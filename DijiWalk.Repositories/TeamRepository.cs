//-----------------------------------------------------------------------
// <copyright file="TeamRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Team to the database
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TeamRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Add</param>
        public void Add(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Team passed in the parameters
        /// </summary>
        /// <param name="team">Object Team to Delete</param>
        public void Delete(Team team)
        {
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Team with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Team</param>
        /// <returns>The Team with the Id researched</returns>
        public Team Find(int id)
        {
            return _context.Teams.Find(id);
        }

        /// <summary>
        /// Method to find all Team from the database
        /// </summary>
        /// <returns>A List with all Teams</returns>
        public IEnumerable<Team> FindAll()
        {
            return _context.Teams;
        }

        /// <summary>
        /// Method that will Update the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Update</param>
        public void Update(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }
    }
}
