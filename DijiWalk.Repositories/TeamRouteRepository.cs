//-----------------------------------------------------------------------
// <copyright file="TeamRouteRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object TeamRoute to the database
    /// </summary>
    public class TeamRouteRepository : ITeamRouteRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the TeamRoute passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Add</param>
        public void Add(TeamRoute teamRoute)
        {
            this._smartCityContext.Teamroutes.Add(teamRoute);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamRoute passed in the parameters
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Delete</param>
        public void Delete(TeamRoute teamRoute)
        {
            this._smartCityContext.Teamroutes.Remove(teamRoute);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an TeamRoute with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the TeamRoute</param>
        /// <returns>The TeamRoute with the Id researched</returns>
        public TeamRoute Find(int id)
        {
            return this._smartCityContext.Teamroutes.Find(id);
        }

        /// <summary>
        /// Method to find all TeamRoute from the database
        /// </summary>
        /// <returns>A List with all TeamRoutes</returns>
        public IEnumerable<TeamRoute> FindAll()
        {
            return this._smartCityContext.Teamroutes;
        }

        /// <summary>
        /// Method that will Update the TeamRoute passed in the parameters to the database
        /// </summary>
        /// <param name="teamRoute">Object TeamRoute to Update</param>
        public void Update(TeamRoute teamRoute)
        {
            this._smartCityContext.Teamroutes.Update(teamRoute);
            this._smartCityContext.SaveChanges();
        }
    }
}
