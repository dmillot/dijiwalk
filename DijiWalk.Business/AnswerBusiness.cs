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
    public class AnswerBusiness : IAnswerBusiness
    {

        private readonly SmartCityContext _context;

        public AnswerBusiness(SmartCityContext context)
        {
            _context = context;
        }



        /// <summary>
        /// Method to Delete all link between question and answer
        /// </summary>
        /// <param name="idTrial">Id of the trial</param>
        public async Task<ApiResponse> DeleteAllFromTrial(int idTrial)
        {
            try
            {
                _context.Answers.RemoveRange(await _context.Answers.Where(x => x.IdTrial == idTrial).ToListAsync());
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to add answer of trial of duplicate mission
        /// </summary>
        /// <param name="answers">List of new answers</param>
        public async Task<ApiResponse> AddFromNewTrialFromMissionFromStep(List<Answer> answers)
        {
            try
            {
                await _context.Answers.AddRangeAsync(answers);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
