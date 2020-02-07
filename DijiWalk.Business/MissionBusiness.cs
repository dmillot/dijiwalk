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
    public class MissionBusiness : IMissionBusiness
    {

        private readonly SmartCityContext _context;

        private readonly ITrialBusiness _trialBusiness;


        public MissionBusiness(SmartCityContext context, ITrialBusiness trialBusiness)
        {
            _context = context;
            _trialBusiness = trialBusiness;
        }



        /// <summary>
        /// Method to Delete all link between step and mission
        /// </summary>
        /// <param name="idStep">Id of the step</param>
        public async Task<ApiResponse> DeleteAllFromStep(int idStep)
        {
            try
            {
                var listMission = await _context.Missions.Where(x => x.IdStep == idStep).ToListAsync();
                listMission.ForEach(async l => await _trialBusiness.DeleteAllFromMission(l.Id));
                _context.Missions.RemoveRange(listMission);
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
