using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;


namespace DijiWalk.Common.Contracts
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(string imageFile, string fileNameForStorage, string extension);
        void DeleteFile(string fileNameForStorage);
    }
}
