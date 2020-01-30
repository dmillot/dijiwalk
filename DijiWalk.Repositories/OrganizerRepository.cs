//-----------------------------------------------------------------------
// <copyright file="OrganizerRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Organizer to the database
    /// </summary>
    public class OrganizerRepository : IOrganizerRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to Add</param>
        public void Add(Organizer organizer)
        {
            this._smartCityContext.Organizers.Add(organizer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Organizer passed in the parameters
        /// </summary>
        /// <param name="organizer">Object Organizer to Delete</param>
        public void Delete(Organizer organizer)
        {
            this._smartCityContext.Organizers.Remove(organizer);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Organizer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Organizer</param>
        /// <returns>The Organizer with the Id researched</returns>
        public Organizer Find(int id)
        {
            return this._smartCityContext.Organizers.Find(id);
        }

        /// <summary>
        /// Method to find all Organizer from the database
        /// </summary>
        /// <returns>A List with all Organizers</returns>
        public IEnumerable<Organizer> FindAll()
        {
            return this._smartCityContext.Organizers;
        }

        /// <summary>
        /// Method that will Update the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to Update</param>
        public void Update(Organizer organizer)
        {
            this._smartCityContext.Organizers.Update(organizer);
            this._smartCityContext.SaveChanges();
        }
    }
}
