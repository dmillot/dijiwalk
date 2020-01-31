//-----------------------------------------------------------------------
// <copyright file="TransportRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Transport to the database
    /// </summary>
    public class TransportRepository : ITransportRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TransportRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Add</param>
        public void Add(Transport transport)
        {
            _context.Transports.Add(transport);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Transport passed in the parameters
        /// </summary>
        /// <param name="transport">Object Transport to Delete</param>
        public void Delete(Transport transport)
        {
            _context.Transports.Remove(transport);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Transport with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Transport</param>
        /// <returns>The Transport with the Id researched</returns>
        public Transport Find(int id)
        {
            return _context.Transports.Find(id);
        }

        /// <summary>
        /// Method to find all Transport from the database
        /// </summary>
        /// <returns>A List with all Transport</returns>
        public IEnumerable<Transport> FindAll()
        {
            return _context.Transports;
        }

        /// <summary>
        /// Method that will Update the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Update</param>
        public void Update(Transport transport)
        {
            _context.Transports.Update(transport);
            _context.SaveChanges();
        }
    }
}
