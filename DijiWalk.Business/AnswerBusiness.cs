using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
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
                _context.SaveChanges();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }
    }
}
