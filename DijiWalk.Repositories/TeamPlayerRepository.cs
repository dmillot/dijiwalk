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
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Add</param>
        public void Add(TeamPlayer teamPlayer)
        {
            this._smartCityContext.Teamplayers.Add(teamPlayer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamPlayer passed in the parameters
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Delete</param>
        public void Delete(TeamPlayer teamPlayer)
        {
            this._smartCityContext.Teamplayers.Remove(teamPlayer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an TeamPlayer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the TeamPlayer</param>
        /// <returns>The TeamPlayer with the Id researched</returns>
        public TeamPlayer Find(int id)
        {
            return this._smartCityContext.Teamplayers.Find(id);
        }

        /// <summary>
        /// Method to find all TeamPlayer from the database
        /// </summary>
        /// <returns>A List with all TeamPlayers</returns>
        public IEnumerable<TeamPlayer> FindAll()
        {
            return this._smartCityContext.Teamplayers;
        }

        /// <summary>
        /// Method that will Update the TeamPlayer passed in the parameters to the database
        /// </summary>
        /// <param name="teamPlayer">Object TeamPlayer to Update</param>
        public void Update(TeamPlayer teamPlayer)
        {
            this._smartCityContext.Teamplayers.Update(teamPlayer);
            this._smartCityContext.SaveChanges();
        }
    }
}
