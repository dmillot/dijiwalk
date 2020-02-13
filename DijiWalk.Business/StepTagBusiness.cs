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
    public class StepTagBusiness : IStepTagBusiness
    {

        private readonly SmartCityContext _context;


        public StepTagBusiness(SmartCityContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method to Add a list of StepTag passed in the parameters to the database
        /// </summary>
        /// <param name="stepTags">List of object StepTag to Add</param>
        public async Task<ApiResponse> AddRange(List<StepTag> stepTags)
        {
            try
            {
                await _context.Steptags.AddRangeAsync(stepTags);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = stepTags };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }



        /// <summary>
        /// Method to Delete all step tag 
        /// </summary>
        /// <param name="stepTags">List of step tags to delete</param>
        public async Task<ApiResponse> DeleteFromStep(List<StepTag> stepTags)
        {
            try
            {
                _context.Steptags.RemoveRange(stepTags);
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
