using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IImageBusiness
    {
        /// <summary>
        /// Method to upload on the google cloud storage
        /// </summary>
        /// <param name="image64">Image convert string 64</param>
        /// <param name="fileName">Name of file</param>
        /// <returns></returns>
        Task<string> UploadImage(string image64, string fileName);


        /// <summary>
        /// Method to delete image on the google cloud storage
        /// </summary>
        /// <param name="pictureUrl">Url of the picture to delete</param>
        void DeleteImage(string pictureUrl);
    }
}
