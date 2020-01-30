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
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Add</param>
        public void Add(Transport transport)
        {
            this._smartCityContext.Transports.Add(transport);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Transport passed in the parameters
        /// </summary>
        /// <param name="transport">Object Transport to Delete</param>
        public void Delete(Transport transport)
        {
            this._smartCityContext.Transports.Remove(transport);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Transport with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Transport</param>
        /// <returns>The Transport with the Id researched</returns>
        public Transport Find(int id)
        {
            return this._smartCityContext.Transports.Find(id);
        }

        /// <summary>
        /// Method to find all Transport from the database
        /// </summary>
        /// <returns>A List with all Transport</returns>
        public IEnumerable<Transport> FindAll()
        {
            return this._smartCityContext.Transports;
        }

        /// <summary>
        /// Method that will Update the Transport passed in the parameters to the database
        /// </summary>
        /// <param name="transport">Object Transport to Update</param>
        public void Update(Transport transport)
        {
            this._smartCityContext.Transports.Update(transport);
            this._smartCityContext.SaveChanges();
        }
    }
}
