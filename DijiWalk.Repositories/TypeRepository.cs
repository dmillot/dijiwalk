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
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TypeRepository(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Add</param>
        public void Add(Type type)
        {
            _context.Types.Add(type);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Type passed in the parameters
        /// </summary>
        /// <param name="type">Object Type to Delete</param>
        public void Delete(Type type)
        {
            _context.Types.Remove(type);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to find an Type with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Type</param>
        /// <returns>The Type with the Id researched</returns>
        public Type Find(int id)
        {
            return _context.Types.Find(id);
        }

        /// <summary>
        /// Method to find all Type from the database
        /// </summary>
        /// <returns>A List with all Type</returns>
        public IEnumerable<Type> FindAll()
        {
            return _context.Types;
        }

        /// <summary>
        /// Method that will Update the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Update</param>
        public void Update(Type type)
        {
            _context.Types.Update(type);
            _context.SaveChanges();
        }
    }
}
