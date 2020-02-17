using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class StepAnalyseBusiness : IStepAnalyseBusiness
    {

        private readonly SmartCityContext _context;


        public StepAnalyseBusiness(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add a list of StepTag passed in the parameters to the database
        /// </summary>
        /// <param name="stepTags">List of object StepTag to Add</param>
        /// <param name="stepValidations">List of object StepValidation to Add</param>
        public async Task<ApiResponse> AddRange(List<StepTag> stepTags, List<StepValidation> stepValidations)
        {
            try
            {
                await _context.Steptags.AddRangeAsync(stepTags);
                await _context.StepValidations.AddRangeAsync(stepValidations);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add};
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }



        /// <summary>
        /// Method to Delete all step analyze
        /// </summary>
        /// <param name="idStep">Id of step</param>
        public async Task<ApiResponse> DeleteFromStep(int idStep)
        {
            try
            {
                _context.Steptags.RemoveRange(await _context.Steptags.Where(st => st.IdStep == idStep).ToListAsync());
                _context.StepValidations.RemoveRange(await _context.StepValidations.Where(sv => sv.IdStep == idStep).ToListAsync());
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }
    }
}
