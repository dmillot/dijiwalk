using DijiWalk.Common.Clouds;
using DijiWalk.Common.Contracts;
using DijiWalk.Entities;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Common.Vision
{
    public class Vision : IVision
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly ImageAnnotatorClient client;

        public Vision(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration["GoogleCredentialFile"]);
            storageClient = StorageClient.Create(googleCredential);
            client = ImageAnnotatorClient.Create();
        }

        /// <summary>
        /// Get all tag for this picture
        /// </summary>
        /// <param name="imageFile">Picture</param>
        /// <param name="extension">Extension picture</param>
        /// <returns>All tag with over 70% of prediction</returns>
        public async Task<List<EntityAnnotation>> GetTags(string imageFile, string extension)
        {
            IReadOnlyList<EntityAnnotation> labels = await client.DetectLabelsAsync(Image.FromBytes(ImageConverter.Base64ToByte(imageFile.Replace($"data:image/{extension};base64,", ""))));
            return labels.Where(l => l.Score * 100 >= 70).ToList();
           
        }

        public void GetWhatIs(string imageFile, string extension)
        {
            WebDetection webDetection = client.DetectWebInformation(Image.FromBytes(ImageConverter.Base64ToByte(imageFile.Replace($"data:image/{extension};base64,", ""))));
            foreach (WebDetection.Types.WebEntity entity in webDetection.WebEntities)
            {
                Console.WriteLine($"Web entity: {entity.EntityId} / {entity.Description} ({entity.Score})");
            }
        }
    }
}
