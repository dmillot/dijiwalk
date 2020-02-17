using DijiWalk.Business.Contracts;
using DijiWalk.Common.Contracts;
using DijiWalk.Common.FileExtension;
using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.EntitiesContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business
{
    public class ImageBusiness : IImageBusiness
    {

        private readonly SmartCityContext _context;

        private readonly ICloudStorage _cloudStorage;

        private readonly IFileExtension _fileExtension;

        private readonly IStringExtension _stringExtension;

        private readonly IVision _vision;

        private readonly IStepAnalyseBusiness _stepAnalyseBusiness;

        private readonly IConfiguration _configuration;

        public ImageBusiness(SmartCityContext context, IConfiguration configuration, ICloudStorage cloudStorage, IFileExtension fileExtension, IStringExtension stringExtension, IVision vision, IStepAnalyseBusiness stepAnalyseBusiness)
        {
            _context = context;
            _cloudStorage = cloudStorage;
            _fileExtension = fileExtension;
            _stringExtension = stringExtension;
            _vision = vision;
            _stepAnalyseBusiness = stepAnalyseBusiness;
            _configuration = configuration;
        }

        /// <summary>
        /// Method to upload on the google cloud storage
        /// </summary>
        /// <param name="image64">Image convert string 64</param>
        /// <param name="fileName">Name of file</param>
        /// <returns></returns>
        public async Task<string> UploadImage(string image64, string fileName)
        {
            var extension = _fileExtension.GetExtension(image64.Substring(0, 50));
            return await _cloudStorage.UploadFileAsync(image64, $"{_stringExtension.RemoveDiacriticsAndWhiteSpace(fileName)}.{extension}", extension);
        }

        /// <summary>
        /// Method to delete image on the google cloud storage
        /// </summary>
        /// <param name="pictureUrl">Url of the picture to delete</param>
        public void DeleteImage(string pictureUrl)
        {
            _cloudStorage.DeleteFile(_fileExtension.GetName(pictureUrl));
        }


        /// <summary>
        /// Method to analyze picture and add some tag
        /// </summary>
        /// <param name="image64">Image convert string 64</param>
        /// <param name="fileName">Name of file</param>
        /// <returns></returns>
        public async Task<ApiResponse> Analyze(string image64, int idStep)
        {
            try
            {
                var tags = await _vision.GetTags(image64, _fileExtension.GetExtension(image64.Substring(0, 50)));
                var listAlreadyCreatedTag = await _context.Tags.Where(t => tags.Select(tag => tag.Description).Contains(t.Name)).ToListAsync();
                var newTags = tags.Where(t => !listAlreadyCreatedTag.Select(a => a.Name).Contains(t.Description)).ToList();
                var listNewTags = newTags.Select(t => new Tag { Id = 0, Name = t.Description, Description = t.Description, Color = null }).ToList();
                await _context.Tags.AddRangeAsync(listNewTags);
                await _context.SaveChangesAsync();
                List<StepTag> stepTags = new List<StepTag>();
                if (listNewTags.Count() != 0)
                    stepTags.AddRange(listNewTags.Select(t => new StepTag { IdStep = idStep, IdTag = t.Id }).ToList());
                if (listAlreadyCreatedTag.Count() != 0)
                    stepTags.AddRange(listAlreadyCreatedTag.Select(t => new StepTag { IdStep = idStep, IdTag = t.Id }).ToList());

                await _stepAnalyseBusiness.AddRange(stepTags, await _vision.GetWhat(image64, _fileExtension.GetExtension(image64.Substring(0, 50)), idStep));
                await _context.SaveChangesAsync();
                return new ApiResponse { Status = ApiStatus.Ok, Message = "Image analyzed" };
            }
            catch (Exception e)
            {
                return TranslateError.Convert(e);
            }

        }

    }

}