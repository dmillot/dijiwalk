//-----------------------------------------------------------------------
// <copyright file="MissionRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Mission to the database
    /// </summary>
    public class MissionRepository : IMissionRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">Object Mission to Add</param>
        public void Add(Mission mission)
        {
            this._smartCityContext.Missions.Add(mission);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Mission passed in the parameters
        /// </summary>
        /// <param name="mission">Object Mission to Delete</param>
        public void Delete(Mission mission)
        {
            this._smartCityContext.Missions.Remove(mission);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Mission with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Mission</param>
        /// <returns>The Mission with the Id researched</returns>
        public Mission Find(int id)
        {
            return this._smartCityContext.Missions.Find(id);
        }

        /// <summary>
        /// Method to find all Mission from the database
        /// </summary>
        /// <returns>A List with all Mission</returns>
        public IEnumerable<Mission> FindAll()
        {
            return this._smartCityContext.Missions;
        }

        /// <summary>
        /// Method that will Update the Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">Object Mission to Update</param>
        public void Update(Mission mission)
        {
            this._smartCityContext.Missions.Update(mission);
            this._smartCityContext.SaveChanges();
        }
    }
}
