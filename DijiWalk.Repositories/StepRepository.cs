//-----------------------------------------------------------------------
// <copyright file="StepRepository.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class that connect the Object Step to the database
    /// </summary>
    public class StepRepository : IStepRepository
    {
        private readonly SmartCityContext _context;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public StepRepository(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to Add the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Add</param>
        public void Add(Step step)
        {
            _context.Steps.Add(step);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete from the database the Step passed in the parameters
        /// </summary>
        /// <param name="idStep">Object Step to Delete</param>
        public async Task<string> Delete(int idStep)
        {
            try
            {
                _context.Steps.Remove(await _context.Steps.FindAsync(idStep));
                _context.SaveChanges();
                return "Ok";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Method to find an Step with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Step</param>
        /// <returns>The Step with the Id researched</returns>
        public async Task<Step> Find(int id)
        {
            return _context.Steps.Find(id);
            //return await _context.Steps.Where(step => step.Id == id).Include(s => s.RouteSteps).FirstOrDefaultAsync();
            // return await _context.Steps.Where(step => step.Id == id).Include(step => step.RouteSteps).ThenInclude(routeStep => routeStep);
        }

        /// <summary>
        /// Method to find all Step from the database
        /// </summary>
        /// <returns>A List with all Steps</returns>
        public async Task<IEnumerable<Step>> FindAll()
        {
            return await _context.Steps.ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Update</param>
        public void Update(Step step)
        {
            _context.Steps.Update(step);
            _context.SaveChanges();
        }
    }
}
