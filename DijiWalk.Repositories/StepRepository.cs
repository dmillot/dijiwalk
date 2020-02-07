//-----------------------------------------------------------------------
// <copyright file="StepRepository.cs" company="DijiWalk">
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
    using DijiWalk.Common.Response;
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

        private readonly IStepBusiness _stepBusiness;

        private readonly IMissionBusiness _missionBusiness;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public StepRepository(SmartCityContext context, IStepBusiness stepBusiness, IMissionBusiness missionBusiness)
        {
            _context = context;
            _stepBusiness = stepBusiness;
            _missionBusiness = missionBusiness;
        }

        /// <summary>
        /// Method to Add the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Add</param>
        public async Task<ApiResponse> Add(Step step)
        {
            try
            {
                await _context.Steps.AddAsync(step);
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = step }; 
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete from the database the Step passed in the parameters
        /// </summary>
        /// <param name="idStep">Object Step to Delete</param>
        public async Task<ApiResponse> Delete(int idStep)
        {
            try
            {
                if(!await _stepBusiness.ContainsStep(idStep))
                {
                    await _missionBusiness.DeleteAllFromStep(idStep);
                    _context.Steps.Remove(await _context.Steps.FindAsync(idStep));
                    _context.SaveChanges();
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
                } else
                {
                    return new ApiResponse { Status = ApiStatus.CantDelete, Message = "Impossible, l'étape est utilisée dans un jeu" };
                }
                
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to find an Step with his Id in the database
        /// </summary>
        /// <param name="id">The Id of the Step</param>
        /// <returns>The Step with the Id researched</returns>
        public async Task<Step> Find(int id)
        {
            return await _context.Steps.Where(s => s.Id == id).Include(s => s.Missions).FirstOrDefaultAsync();
            // return await _context.Steps.Where(step => step.Id == id).Include(s => s.RouteSteps).FirstOrDefaultAsync();
            // return await _context.Steps.Where(step => step.Id == id).Include(step => step.RouteSteps).ThenInclude(routeStep => routeStep);
        }

        /// <summary>
        /// Method to find all Step from the database
        /// </summary>
        /// <returns>A List with all Steps</returns>
        public async Task<IEnumerable<Step>> FindAll()
        {
            return await _context.Steps.Include(s => s.Missions).ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Update</param>
        public async Task<ApiResponse> Update(Step step)
        {
            try
            {
                _context.Steps.Update(step);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = step };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
