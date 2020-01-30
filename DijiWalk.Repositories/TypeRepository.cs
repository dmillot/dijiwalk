//-----------------------------------------------------------------------
// <copyright file="TypeRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Type to the database
    /// </summary>
    public class TypeRepository : ITypeRepository
    {
        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        SmartCityContext _smartCityContext = new SmartCityContext();

        /// <summary>
        /// Method to Add the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Add</param>
        public void Add(Type type)
        {
            this._smartCityContext.Types.Add(type);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Type passed in the parameters
        /// </summary>
        /// <param name="type">Object Type to Delete</param>
        public void Delete(Type type)
        {
            this._smartCityContext.Types.Remove(type);
            this._smartCityContext.SaveChanges();
        }

        /// <summary>
        /// Method to find an Type with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Type</param>
        /// <returns>The Type with the Id researched</returns>
        public Type Find(int id)
        {
            return this._smartCityContext.Types.Find(id);
        }

        /// <summary>
        /// Method to find all Type from the database
        /// </summary>
        /// <returns>A List with all Type</returns>
        public IEnumerable<Type> FindAll()
        {
            return this._smartCityContext.Types;
        }

        /// <summary>
        /// Method that will Update the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Update</param>
        public void Update(Type type)
        {
            this._smartCityContext.Types.Update(type);
            this._smartCityContext.SaveChanges();
        }
    }
}
