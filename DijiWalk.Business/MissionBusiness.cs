using DijiWalk.Business.Contracts;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using DijiWalk.Repositories.Contracts;
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

        private readonly IMissionRepository _missionRepository;


        public MissionBusiness(SmartCityContext context, ITrialBusiness trialBusiness, IMissionRepository missionRepository)
        {
            _context = context;
            _trialBusiness = trialBusiness;
            _missionRepository = missionRepository;
        }

        /// <summary>
        /// Method to Add a list of Mission passed in the parameters to the database
        /// </summary>
        /// <param name="mission">List of object Mission to Add</param>
        public async Task<ApiResponse> AddRange(List<Mission> missions)
        {
            try
            {
                List<Mission> newMissions = this.SeparateTrial(missions);
                await _context.Missions.AddRangeAsync(newMissions);
                await _context.SaveChangesAsync();
                List<Trial> trials = new List<Trial>();
                missions.ForEach(m =>
                {
                    trials.AddRange(m.Trials.Select(t =>
                    {
                        t.IdMission = newMissions[missions.IndexOf(m)].Id;
                        t.Id = 0;
                        return t;
                    }));
                });
                var response = await _trialBusiness.AddFromNewMissionFromStep(trials);
                if (response.Status == ApiStatus.Ok)
                {
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = missions };
                }
                else
                    return response;
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }



        /// <summary>
        /// Method to Delete all link between step and mission
        /// </summary>
        /// <param name="idStep">Id of the step</param>
        public async Task<ApiResponse> DeleteAllFromStep(Step step)
        {
            try
            {
                var listMission = await _context.Missions.Where(x => x.IdStep == step.Id).ToListAsync();
                listMission.ForEach(async l => await _trialBusiness.DeleteAllFromMission(l.Id));
                _context.Missions.RemoveRange(listMission);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to Delete all link between step and mission
        /// </summary>
        /// <param name="missionToDelete">List of mission to delete</param>
        public async Task<ApiResponse> DeleteNotFromStep(List<Mission> missionToDelete)
        {
            try
            {
                _context.Missions.RemoveRange(missionToDelete);
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to separate mission and trials 
        /// </summary>
        /// <param name="mission">Object mission</param>
        public List<Mission> SeparateTrial(List<Mission> missions)
        {

            return missions.Select(m => new Mission(m)).ToList();
        }

        /// <summary>
        /// Method to set up list of mission to add to a step
        /// </summary>
        /// <param name="mission">Object list of missions to set up</param>
        /// <param name="idStep">Id of the step</param>
        public async Task<ApiResponse> SetUp(List<Mission> missions, int idStep)
        {
            return await this.AddRange(missions.Select(m =>
            {
                m.IdStep = idStep;
                m.Id = 0;
                return m;
            }).ToList());
        }

    }
}
