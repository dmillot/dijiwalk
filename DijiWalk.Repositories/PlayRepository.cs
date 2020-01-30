//-----------------------------------------------------------------------
// <copyright file="PlayRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Play to the database
    /// </summary>
    public class PlayRepository : IPlayRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Play passed in the parameters to the database
        /// </summary>
        /// <param name="play">Object Play to Add</param>
        public void Add(Play play)
        {
            this._smartCityContext.Plays.Add(play);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Play passed in the parameters
        /// </summary>
        /// <param name="play">Object Play to Delete</param>
        public void Delete(Play play)
        {
            this._smartCityContext.Plays.Remove(play);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Play with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Play</param>
        /// <returns>The Play with the Id researched</returns>
        public Play Find(int id)
        {
            return this._smartCityContext.Plays.Find(id);
        }

        /// <summary>
        /// Method to find all Play from the database
        /// </summary>
        /// <returns>A List with all Plays</returns>
        public IEnumerable<Play> FindAll()
        {
            return this._smartCityContext.Plays;
        }

        /// <summary>
        /// Method that will Update the Play passed in the parameters to the database
        /// </summary>
        /// <param name="play">Object Play to Update</param>
        public void Update(Play play)
        {
            this._smartCityContext.Plays.Update(play);
            this._smartCityContext.SaveChanges();
        }
    }
}
