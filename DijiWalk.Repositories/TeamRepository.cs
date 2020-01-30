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
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Add</param>
        public void Add(Team team)
        {
            this._smartCityContext.Teams.Add(team);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Team passed in the parameters
        /// </summary>
        /// <param name="team">Object Team to Delete</param>
        public void Delete(Team team)
        {
            this._smartCityContext.Teams.Remove(team);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Team with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Team</param>
        /// <returns>The Team with the Id researched</returns>
        public Team Find(int id)
        {
            return this._smartCityContext.Teams.Find(id);
        }

        /// <summary>
        /// Method to find all Team from the database
        /// </summary>
        /// <returns>A List with all Teams</returns>
        public IEnumerable<Team> FindAll()
        {
            return this._smartCityContext.Teams;
        }

        /// <summary>
        /// Method that will Update the Team passed in the parameters to the database
        /// </summary>
        /// <param name="team">Object Team to Update</param>
        public void Update(Team team)
        {
            this._smartCityContext.Teams.Update(team);
            this._smartCityContext.SaveChanges();
        }
    }
}
