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
        Task<bool> ValidationImage(Validate validate);
    }
}
