//-----------------------------------------------------------------------
// <copyright file="TagRepository.cs" company="DijiWalk">
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
    /// Class that connect the Object Tag to the database
    /// </summary>
    public class TagRepository : ITagRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TagRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Add</param>
        public void Add(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Tag passed in the parameters
        /// </summary>
        /// <param name="tag">Object Tag to Delete</param>
        public async Task<ApiResponse> Delete(int idTag)
        {
            try
            {
                _context.Tags.Remove(await _context.Tags.FindAsync(idTag));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

        /// <summary>
        /// Method to find an Tag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Tag</param>
        /// <returns>The Tag with the Id researched</returns>
        public async Task<Tag> Find(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        /// <summary>
        /// Method to find all Tag from the database
        /// </summary>
        /// <returns>A List with all Tag</returns>
        public async Task<IEnumerable<Tag>> FindAll()
        {
            return await _context.Tags.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Update</param>
        public void Update(Tag tag)
        {
            _context.Tags.Update(tag);
            _context.SaveChanges();
        }
    }
}
