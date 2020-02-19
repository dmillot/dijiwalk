using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Common;
using DijiWalk.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.Services
{
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Method to upload on the google cloud storage
        /// </summary>
        /// <param name="image64">Image convert string 64</param>
        /// <param name="fileName">Name of file</param>
        /// <returns></returns>
        public async Task<bool> ValidationImage(Validate validate)
        {
            return JsonConvert.DeserializeObject<bool>(await CommonService.Post("step/validate", validate));
        }
    }
}
