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
    public class TrialBusiness : ITrialBusiness
    {

        private readonly SmartCityContext _context;

        private readonly IAnswerBusiness _answerBusiness;

        public TrialBusiness(SmartCityContext context, IAnswerBusiness answerBusiness)
        {
            _context = context;
            _answerBusiness = answerBusiness;
        }



        /// <summary>
        /// Method to Delete all link between mission and question
        /// </summary>
        /// <param name="idMission">Id of the mission</param>
        public async Task<ApiResponse> DeleteAllFromMission(int idMission)
        {
            try
            {
                var listTrials = await _context.Trials.Where(x => x.IdMission == idMission).ToListAsync();
                listTrials.ForEach(async l => await _answerBusiness.DeleteAllFromTrial(l.Id));
                _context.Trials.RemoveRange(listTrials);
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
