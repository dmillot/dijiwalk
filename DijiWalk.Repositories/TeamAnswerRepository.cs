//-----------------------------------------------------------------------
// <copyright file="TeamAnswerRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object TeamAnswer to the database
    /// </summary>
    public class TeamAnswerRepository : ITeamAnswerRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the TeamAnswer passed in the parameters to the database
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Add</param>
        public void Add(TeamAnswer teamAnswer)
        {
            this._smartCityContext.Teamanswers.Add(teamAnswer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the TeamAnswer passed in the parameters
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Delete</param>
        public void Delete(TeamAnswer teamAnswer)
        {
            this._smartCityContext.Teamanswers.Remove(teamAnswer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an TeamAnswer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the TeamAnswer</param>
        /// <returns>The TeamAnswer with the Id researched</returns>
        public TeamAnswer Find(int id)
        {
            return this._smartCityContext.Teamanswers.Find(id);
        }

        /// <summary>
        /// Method to find all TeamAnswer from the database
        /// </summary>
        /// <returns>A List with all TeamAnswers</returns>
        public IEnumerable<TeamAnswer> FindAll()
        {
            return this._smartCityContext.Teamanswers;
        }

        /// <summary>
        /// Method that will Update the TeamAnswer passed in the parameters to the database
        /// </summary>
        /// <param name="teamAnswer">Object TeamAnswer to Update</param>
        public void Update(TeamAnswer teamAnswer)
        {
            this._smartCityContext.Teamanswers.Update(teamAnswer);
            this._smartCityContext.SaveChanges();
        }
    }
}
