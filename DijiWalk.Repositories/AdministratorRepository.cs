//-----------------------------------------------------------------------
// <copyright file="AdministratorRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Administrator to the database
    /// </summary>
    public class AdministratorRepository : IAdministratorRepository
    {

        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public AdministratorRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Administrator passed in the parameters to the database
        /// </summary>
        /// <param name="administrator">Object Administrator to Add</param>
        public void Add(Administrator administrator)
        {
           _context.Administrators.Add(administrator);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Administrator passed in the parameters
        /// </summary>
        /// <param name="administrator">Object Administrator to Delete</param>
        public void Delete(Administrator administrator)
        {
           _context.Administrators.Remove(administrator);
           _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Administrator with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Administrator</param>
        /// <returns>The Administrator with the Id researched</returns>
        public Administrator Find(int id)
        {
            return _context.Administrators.Find(id);
        }

        /// <summary>
        /// Method to find all Administrator from the database
        /// </summary>
        /// <returns>A List with all Administrators</returns>
        public IEnumerable<Administrator> FindAll()
        {
            return _context.Administrators;
        }

        /// <summary>
        /// Method that will Update the Administrator passed in the parameters to the database
        /// </summary>
        /// <param name="administrator">Object Administrator to Update</param>
        public void Update(Administrator administrator)
        {
           _context.Administrators.Update(administrator);
           _context.SaveChanges();
        }
    }
}
