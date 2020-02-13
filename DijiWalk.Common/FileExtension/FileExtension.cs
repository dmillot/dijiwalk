using DijiWalk.Common.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijiWalk.Common.FileExtension
{
    public class FileExtension : IFileExtension
    {
        /// <summary>
        /// List of extension
        /// </summary>
        private List<string> ListExtension
        {
            get
            {
                return new List<string> { "png", "jpg", "jpeg", "gif" };
            }
        }


        private readonly IConfiguration _configuration;

        public FileExtension(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get extension of file
        /// </summary>
        /// <param name="base64">File base64</param>
        /// <returns>Extension of file</returns>
        public string GetExtension(string base64)
        {
            return ListExtension.Where(ext => base64.Contains($"image/{ext}")).FirstOrDefault();
        }

        /// <summary>
        /// Method to get name of file
        /// </summary>
        /// <param name="pictureUrl">URL of the picture</param>
        /// <returns>Name of file</returns>
        public string GetName(string pictureUrl)
        {
            if(pictureUrl.IndexOf("?") != -1)
            {
                return pictureUrl.Substring(pictureUrl.IndexOf("/o/") + 3, pictureUrl.Substring(pictureUrl.IndexOf("/o/") + 3).IndexOf("?"));

            } else
            {
                return pictureUrl.Substring(pictureUrl.IndexOf($"{_configuration["GoogleCloudStorageBucket"]}/") + _configuration["GoogleCloudStorageBucket"].Length + 1);
            }
        }
    }
}
