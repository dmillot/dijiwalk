using DijiWalk.Business.Contracts;
using DijiWalk.Common.Contracts;
using DijiWalk.Common.FileExtension;
using DijiWalk.EntitiesContext;
using System;
using System.Collections.Generic;
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

        public ImageBusiness(SmartCityContext context, ICloudStorage cloudStorage, IFileExtension fileExtension, IStringExtension stringExtension)
        {
            _context = context;
            _cloudStorage = cloudStorage;
            _fileExtension = fileExtension;
            _stringExtension = stringExtension;
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

    }
}
