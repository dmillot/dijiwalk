//-----------------------------------------------------------------------
// <copyright file="TagRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Business.Contracts;
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

        private readonly ITagBusiness _tagBusiness;

        private readonly IRouteTagRepository _routeTagRepository;


        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public TagRepository(SmartCityContext context, ITagBusiness tagBusiness, IRouteTagRepository routeTagRepository)
        {
            _context = context;
            _tagBusiness = tagBusiness;
            _routeTagRepository = routeTagRepository;
        }

        /// <summary>
        /// Method to Add the Tag passed in the parameters to the database
        /// </summary>
        /// <param name="tag">Object Tag to Add</param>
        public async Task<ApiResponse> Add(Tag tag)
        {
            try
            {
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = tag };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
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
