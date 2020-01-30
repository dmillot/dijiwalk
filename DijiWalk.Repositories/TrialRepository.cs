//-----------------------------------------------------------------------
// <copyright file="TrialRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Trial to the database
    /// </summary>
    public class TrialRepository : ITrialRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Trial passed in the parameters to the database
        /// </summary>
        /// <param name="trial">Object Trial to Add</param>
        public void Add(Trial trial)
        {
            this._smartCityContext.Trials.Add(trial);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Trial passed in the parameters
        /// </summary>
        /// <param name="trial">Object Trial to Delete</param>
        public void Delete(Trial trial)
        {
            this._smartCityContext.Trials.Remove(trial);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Trial with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Trial</param>
        /// <returns>The Trial with the Id researched</returns>
        public Trial Find(int id)
        {
            return this._smartCityContext.Trials.Find(id);
        }

        /// <summary>
        /// Method to find all Trial from the database
        /// </summary>
        /// <returns>A List with all Trial</returns>
        public IEnumerable<Trial> FindAll()
        {
            return this._smartCityContext.Trials;
        }

        /// <summary>
        /// Method that will Update the Trial passed in the parameters to the database
        /// </summary>
        /// <param name="trial">Object Trial to Update</param>
        public void Update(Trial trial)
        {
            this._smartCityContext.Trials.Update(trial);
            this._smartCityContext.SaveChanges();
        }
    }
}
