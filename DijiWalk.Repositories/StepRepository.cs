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
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.EntitiesContext;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Xamarin.Forms.Internals;

    /// <summary>
    /// Class that connect the Object Step to the database
    /// </summary>
    public class StepRepository : IStepRepository
    {
        private readonly SmartCityContext _context;

        private readonly IStepBusiness _stepBusiness;

        private readonly IMissionBusiness _missionBusiness;

        private readonly IImageBusiness _imageBusiness;

        private readonly IStepAnalyseBusiness _stepAnalyseBusiness;

        private readonly IVision _vision;

        /// <summary>
        /// Parameter that serve to connect to the database
        /// </summary>
        public StepRepository(SmartCityContext context, IVision vision, IStepBusiness stepBusiness, IMissionBusiness missionBusiness, IImageBusiness imageBusiness, IStepAnalyseBusiness stepAnalyseBusiness)

        {
            _vision = vision;
            _context = context;
            _stepBusiness = stepBusiness;
            _missionBusiness = missionBusiness;
            _imageBusiness = imageBusiness;
            _stepAnalyseBusiness = stepAnalyseBusiness;
        }

        /// <summary>
        /// Method to Add the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Add</param>
        public async Task<ApiResponse> Add(Step step)
        {
            try
            {
                var newStep = _stepBusiness.SeparateStep(step);
                newStep.Validation = await _imageBusiness.UploadImage(newStep.ImageBase64, $"{newStep.Name}-{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                var oldIdMissions = step.Missions.Select(m => { return m.Id; }).ToList();
                var missions = await _context.Missions.AsNoTracking().Where(m => oldIdMissions.Contains((int)m.IdStep)).Include(m => m.Trials).ThenInclude(t => t.Answers).ToListAsync();
                await _context.Steps.AddAsync(newStep);
                await _context.SaveChangesAsync();
                await _imageBusiness.Analyze(newStep.ImageBase64, newStep.Id);
                var response = await _missionBusiness.SetUp(missions, newStep.Id);

                if (response.Status == ApiStatus.Ok)
                {
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Add, Response = await _context.Steps.Where(s => s.Id == newStep.Id).Include(s => s.Missions).FirstOrDefaultAsync() };
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
        /// Method to Delete from the database the Step passed in the parameters
        /// </summary>
        /// <param name="idStep">Object Step to Delete</param>
        public async Task<ApiResponse> Delete(int idStep)
        {
            try
            {
                if (!await _stepBusiness.ContainsStep(idStep))
                {
                    var step = await _context.Steps.Where(s => s.Id == idStep).Include(s => s.Missions).ThenInclude(m => m.Trials).ThenInclude(t => t.Answers).Include(s => s.StepTags).FirstOrDefaultAsync();
                    if (step.Validation != null)
                    {
                        _imageBusiness.DeleteImage(step.Validation);
                    }
                    _context.Steps.Remove(step);
                    await _context.SaveChangesAsync();
                    return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Delete };
                }
                else
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
            return await _context.Steps.Where(s => s.Id == id).Include(s => s.Missions).ThenInclude(m => m.Trials).Include(s => s.StepTags).ThenInclude(st => st.Tag).Include(s => s.Clues).FirstOrDefaultAsync();

            // return await _context.Steps.Where(step => step.Id == id).Include(s => s.RouteSteps).FirstOrDefaultAsync();
            // return await _context.Steps.Where(step => step.Id == id).Include(step => step.RouteSteps).ThenInclude(routeStep => routeStep);
        }

        /// <summary>
        /// Method to find all Step from the database
        /// </summary>
        /// <returns>A List with all Steps</returns>
        public async Task<IEnumerable<Step>> FindAll()
        {
            return await _context.Steps
                .Include(s => s.Missions)
                .ThenInclude(m => m.Trials)
                .Include(s => s.StepTags)
                .ThenInclude(st => st.Tag)
                .ToListAsync();
        }

        /// <summary>
        /// Method that will Update the Step passed in the parameters to the database
        /// </summary>
        /// <param name="step">Object Step to Update</param>
        public async Task<ApiResponse> Update(Step step)
        {
            try
            {
                if (step.ImageChanged)
                {
                    if (step.Validation != null)
                    {
                        _imageBusiness.DeleteImage(step.Validation);
                        await _stepAnalyseBusiness.DeleteFromStep(step.Id);
                    }
                    step.Validation = await _imageBusiness.UploadImage(step.ImageBase64, $"{step.Name}-{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                    await _imageBusiness.Analyze(step.ImageBase64, step.Id);
                }
                _context.Steps.Update(step);
                
                await _context.SaveChangesAsync();

                var oldIdMissions = _context.Missions.Where(m => m.IdStep == step.Id).Select(m => m.Id).ToList();
                var idMissions = step.Missions.Select(m => m.Id).ToList();
                var newIdMissions = idMissions.Where(m => !oldIdMissions.Contains(m)).ToList();

                var responseDelete = await _missionBusiness.DeleteNotFromStep(await _context.Missions.Where(m => m.IdStep == step.Id && !idMissions.Contains(m.Id)).Include(m => m.Trials).ThenInclude(t => t.Answers).ToListAsync());
                if (responseDelete.Status == ApiStatus.Ok)
                {
                    var missions = await _context.Missions.AsNoTracking().Where(m => newIdMissions.Contains((int)m.IdStep)).Include(m => m.Trials).ThenInclude(t => t.Answers).ToListAsync();
                    var responseAdd = await _missionBusiness.SetUp(missions, step.Id);
                    if (responseAdd.Status == ApiStatus.Ok)
                    {
                        return new ApiResponse { Status = ApiStatus.Ok, Message = ApiAction.Update, Response = await this.Find(step.Id) };
                    }
                    else
                        return responseAdd;
                }
                else
                    return responseDelete;
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }
        }

        public async Task<bool> Validate(Validate validate)
        {
            var urlPicture = await _imageBusiness.UploadImage(validate.Picture, validate.Filename);
            var guidAllFaces = await _vision.GetFacesId(urlPicture);
            var captain = await _context.Players.FirstOrDefaultAsync(p => p.Id == validate.IdPlayer);
            var guidCaptain = await _vision.GetFacesIdCaptain(captain.Picture);

            var result = await _vision.CompareFaces(guidCaptain, guidAllFaces);

            if (result == true)
            {
                return _vision.CompareLandMarks(await _stepBusiness.GetAnalyzeStep(validate.IdStep), await _vision.GetWhat(validate.Picture, "jpg", validate.IdStep), await _vision.GetTags(validate.Picture, "jpg"));
            }
            else
                return false;
        }
    }
}
