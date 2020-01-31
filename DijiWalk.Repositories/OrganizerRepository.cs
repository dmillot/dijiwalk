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
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public OrganizerRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to Add</param>
        public void Add(Organizer organizer)
        {
           _context.Organizers.Add(organizer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Organizer passed in the parameters
        /// </summary>
        /// <param name="organizer">Object Organizer to Delete</param>
        public void Delete(Organizer organizer)
        {
           _context.Organizers.Remove(organizer);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Organizer with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Organizer</param>
        /// <returns>The Organizer with the Id researched</returns>
        public Organizer Find(int id)
        {
            return _context.Organizers.Find(id);
        }

        /// <summary>
        /// Method to find all Organizer from the database
        /// </summary>
        /// <returns>A List with all Organizers</returns>
        public IEnumerable<Organizer> FindAll()
        {
            return _context.Organizers;
        }

        /// <summary>
        /// Method that will Update the Organizer passed in the parameters to the database
        /// </summary>
        /// <param name="organizer">Object Organizer to Update</param>
        public void Update(Organizer organizer)
        {
           _context.Organizers.Update(organizer);
           _context.SaveChanges();
        }
    }
}
