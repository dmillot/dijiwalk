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
        void GetWhatIs(string imageFile, string extension);
    }
}
