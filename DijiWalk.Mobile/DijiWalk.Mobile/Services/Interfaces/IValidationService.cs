using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services.Interfaces
{
    public interface IValidationService
    {
        /// <summary>
        /// Method to upload on the google cloud storage
        /// </summary>
        /// <param name="image64">Image convert string 64</param>
        /// <param name="fileName">Name of file</param>
        /// <returns></returns>
        Task<bool> ValidationImage(string image64, string fileName);
    }
}
