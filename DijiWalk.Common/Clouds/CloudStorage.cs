
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using DijiWalk.Common.Contracts;
using DijiWalk.Common.Clouds;
using System;

namespace DijiWalk.Services
{
    public class CloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public CloudStorage(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration["GoogleCredentialFile"]);
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration["GoogleCloudStorageBucket"];
        }

        public async Task<string> UploadFileAsync(string imageFile, string fileNameForStorage, string extension)
        {
            var memoryFile = new MemoryStream(ImageConverter.Base64ToByte(imageFile.Replace($"data:image/{extension};base64,","")));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var image = Image.FromStream(memoryFile);
                image.Save(memoryStream, image.RawFormat);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }

        public void DeleteFile(string fileNameForStorage)
        {
            try
            {
                if(storageClient.GetObject(bucketName, fileNameForStorage) != null) storageClient.DeleteObject(bucketName, fileNameForStorage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}