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
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        /// <summary>
        /// Method to add trial of duplicate mission
        /// </summary>
        /// <param name="trials">List of trials</param>
        /// <param name="oldTrials">Id of old trials</param>
        public async Task<ApiResponse> AddFromNewMissionFromStep(List<Trial> trials)
        {
            try
            {
                List<Trial> newTrials = this.SeparateAnswer(trials);
                await _context.Trials.AddRangeAsync(newTrials);
                await _context.SaveChangesAsync();
                List<Answer> answers = new List<Answer>();
                trials.ForEach(t => {
                    answers.AddRange(t.Answers.Select(a =>
                    {
                        a.IdTrial = newTrials[trials.IndexOf(t)].Id;
                        a.Trial = newTrials[trials.IndexOf(t)];
                        a.Id = 0;
                        return a;
                    }));
                });
                var response = await _answerBusiness.AddFromNewTrialFromMissionFromStep(answers);
                if (response.Status == ApiStatus.Ok)
                {
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add };
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
        /// Method to add answer of trial of duplicate mission
        /// </summary>
        /// <param name="trials">List of new answers</param>
        public List<Trial> SeparateAnswer(List<Trial> trials)
        {
            return trials.Select(t => new Trial(t)).ToList();
        }
    }
}
