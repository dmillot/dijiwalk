using DijiWalk.Common.Response;
using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Common;
using DijiWalk.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        public async Task<ApiResponse> ValidationImage(Validate validate)
        {
            return JsonConvert.DeserializeObject<ApiResponse>(await CommonService.Post(String.Concat(Application.Current.Properties["url"], "step/validate"), validate));
        }

        /// <summary>
        /// Method to check if organisateur answer
        /// </summary>
        /// <param name="validate">Validate</param>
        public async Task<ApiResponse> CheckValidation(Validate validate)
        {
            return JsonConvert.DeserializeObject<ApiResponse>(await CommonService.Post(String.Concat(Application.Current.Properties["url"], "step/check"), validate));
        }

    }
}
