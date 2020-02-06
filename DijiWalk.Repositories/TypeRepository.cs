//-----------------------------------------------------------------------
// <copyright file="TypeRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

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
        public void Add(Entities.Type type)
        {
            _context.Types.Add(type);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Type passed in the parameters
        /// </summary>
        /// <param name="type">Object Type to Delete</param>
        public async Task<ApiResponse> Delete(int idType)
        {
            try
            {
                _context.Types.Remove(await _context.Types.FindAsync(idType));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an Type with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Type</param>
        /// <returns>The Type with the Id researched</returns>
        public async Task<Entities.Type> Find(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Type from the database
        /// </summary>
        /// <returns>A List with all Type</returns>
        public async Task<IEnumerable<Entities.Type>> FindAll()
        {
            return await _context.Types.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Type passed in the parameters to the database
        /// </summary>
        /// <param name="type">Object Type to Update</param>
        public void Update(Entities.Type type)
        {
            _context.Types.Update(type);
            _context.SaveChanges();
        }
    }
}
