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
using Xamarin.Forms.Internals;

namespace DijiWalk.Business
{
    public class StepBusiness : IStepBusiness
    {

        private readonly SmartCityContext _context;



        public StepBusiness(SmartCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to check if a step is already in game
        /// </summary>
        /// <param name="idStep">id of a step</param>
        public async Task<bool> ContainsStep(int idStep)
        {
            return await _context.Games.AnyAsync(g => _context.Routesteps.Where(r => r.IdStep == idStep).Any(route => route.IdRoute == g.IdRoute));
        }

        /// <summary>
        /// Method to separate step and mission 
        /// </summary>
        /// <param name="missions">Object List mission</param>
        public Step SeparateStep(Step step)
        {
            return new Step(step);
        }

        /// <summary>
        /// Method to get validation of step 
        /// </summary>
        /// <param name="idStep">id of the step to compare</param>
        public async Task<Step> GetAnalyzeStep(int idStep)
        {
            var step = await _context.Steps.Where(s => s.Id == idStep).Include(s => s.StepTags).ThenInclude(st => st.Tag).Include(s => s.StepValidations).FirstOrDefaultAsync();
            step.RouteSteps = new HashSet<RouteStep>();
            step.Missions = new HashSet<Mission>();
            step.Clues = new HashSet<Clue>();
            step.StepTags = step.StepTags.Select(st =>
            {
                st.Step = null;
                st.Tag.RouteTags = new HashSet<RouteTag>();
                st.Tag.StepTags = new HashSet<StepTag>();
                return st;
            }).ToList();
            step.StepValidations = step.StepValidations.Select(sv =>
            {
                sv.Step = null;
                return sv;
            }).ToList();
            return step;
        }

    }
}
