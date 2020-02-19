using DijiWalk.Entities;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;


namespace DijiWalk.Common.Contracts
{
    public interface IVision
    {
        /// <summary>
        /// Get all tag for this picture
        /// </summary>
        /// <param name="imageFile">Picture</param>
        /// <param name="extension">Extension picture</param>
        /// <returns>All tag with over 70% of prediction</returns>
        Task<List<EntityAnnotation>> GetTags(string imageFile, string extension);
        void GetWeb(string imageFile, string extension);

        /// <summary>
        /// Get all landmarks for this picture
        /// </summary>
        /// <param name="imageFile">Picture</param>
        /// <param name="extension">Extension picture</param>
        /// <returns>All landmarks with over 70% of prediction</returns>
        Task<List<StepValidation>> GetWhat(string imageFile, string extension, int idStep);

        /// <summary>
        /// Get face id of a photo
        /// </summary>
        /// <param name="urlPicture">Url of the photo you want to check if face</param>
        /// <returns>List of face-id unique on the photo</returns>
        Task<List<Guid?>> GetFacesId(string urlPicture);

        /// <summary>
        /// Get face id of a photo of captain
        /// </summary>
        /// <param name="urlPicture">Url of the photo you want to check if face</param>
        /// <returns>List of face-id unique on the photo</returns>
        Task<Guid?> GetFacesIdCaptain(string urlPicture);

        /// <summary>
        /// Compare two photo / face id
        /// </summary>
        /// <param name="capitaineFace">Face id of the capitaine of the team</param>
        /// <param name="facesValidation">All faces id find on the validation photo</param>
        /// <returns>bool: true if similar, false if not similar</returns>
        Task<bool> CompareFaces(Guid? capitaineFace, List<Guid?> facesValidation);

        /// <summary>
        /// Compare two landmarks
        /// </summary>
        /// <param name="Step">Id of the step to compare</param>
        /// <param name="landmarksAnalyze">List of StepValidation landmarks</param>
        /// <param name="tagsAnalyze">List of tags analyze</param>
        /// <returns>bool: true if ok, false if not ok</returns>
        bool CompareLandMarks(Step stepToCompare, List<StepValidation> landmarksAnalyze, List<EntityAnnotation> tagsAnalyze);
    }
}
