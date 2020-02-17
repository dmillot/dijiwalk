//-----------------------------------------------------------------------
// <copyright file="StepTagRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using DijiWalk.Common.Response;

    /// <summary>
    /// Class that connect the Object RouteTag to the database
    /// </summary>
    public class StepTagRepository : IStepTagRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public StepTagRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the stepTag passed in the parameters to the database
        /// </summary>
        /// <param name="stepTag">Object stepTag to Add</param>
        public void Add(StepTag stepTag)
        {
            _context.Steptags.Add(stepTag);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the idStepTag passed in the parameters
        /// </summary>
        /// <param name="idStepTag">Object idStepTag to Delete</param>
        public async Task<ApiResponse> Delete(int idStepTag)
        {
            try
            {
                _context.Routetags.Remove(await _context.Routetags.FindAsync(idStepTag));
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to find an StepTag with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the StepTag</param>
        /// <returns>The StepTag with the Id researched</returns>
        public async Task<StepTag> Find(int id)
        {
            return await _context.Steptags.FindAsync(id);
        }

        /// <summary>
        /// Method to find all StepTag from the database
        /// </summary>
        /// <returns>A List with all StepTag</returns>
        public async Task<IEnumerable<StepTag>> FindAll()
        {
            return await _context.Steptags.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the StepTag passed in the parameters to the database
        /// </summary>
        /// <param name="routeTag">Object StepTag to Update</param>
        public void Update(StepTag stepTag)
        {
            _context.Steptags.Update(stepTag);
            _context.SaveChanges();
        }
    }
}
