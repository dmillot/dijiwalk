using DijiWalk.Common.Response;
using DijiWalk.Entities;
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
        /// <param name="validate">object validate</param>
        /// <returns></returns>
        Task<ApiResponse> ValidationImage(Validate validate);

        /// <summary>
        /// Method to check if organisateur answer
        /// </summary>
        /// <param name="validate">Validate</param>
        Task<ApiResponse> CheckValidation(Validate validate);
    }
}
