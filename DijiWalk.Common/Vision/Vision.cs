﻿using DijiWalk.Common.Clouds;
using DijiWalk.Common.Contracts;
using DijiWalk.Entities;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
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
        private readonly ImageAnnotatorClient clientGoogle;
        private readonly FaceClient clientAzure;

        public Vision(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration["GoogleCredentialFile"]);
            storageClient = StorageClient.Create(googleCredential);
            clientGoogle = ImageAnnotatorClient.Create();
            clientAzure = new FaceClient(new ApiKeyServiceClientCredentials(configuration["AzureKey"])) { Endpoint = configuration["FaceDetectURL"] };
        }

        /// <summary>
        /// Get face id of a photo
        /// </summary>
        /// <param name="urlPicture">Url of the photo you want to check if face</param>
        /// <returns>List of face-id unique on the photo</returns>
        public async Task<IAsyncEnumerable<Guid?>> GetFacesId(string urlPicture)
        {
            var faces = await clientAzure.Face.DetectWithUrlAsync($"{urlPicture}", recognitionModel: RecognitionModel.Recognition01);
            return faces.Select(f => f.FaceId).ToAsyncEnumerable();
        }

        /// <summary>
        /// Compare two photo / face id
        /// </summary>
        /// <param name="capitaineFace">Face id of the capitaine of the team</param>
        /// <param name="facesValidation">All faces id find on the validation photo</param>
        /// <returns>bool: true if similar, false if not similar</returns>
        public async Task<bool> CompareFaces(List<Guid?> capitaineFace, List<Guid?> facesValidation)
        {
            IList<SimilarFace> similarResults = await clientAzure.Face.FindSimilarAsync(capitaineFace.Select(t => (Guid)t).First(),faceIds: facesValidation, maxNumOfCandidatesReturned: 10, mode: FindSimilarMatchMode.MatchPerson);
            return similarResults.Count != 0 ? true : false;
        }

        /// <summary>
        /// Get all tag for this picture
        /// </summary>
        /// <param name="imageFile">Picture</param>
        /// <param name="extension">Extension picture</param>
        /// <returns>All tag with over 70% of prediction</returns>
        public async Task<List<EntityAnnotation>> GetTags(string imageFile, string extension)
        {
            IReadOnlyList<EntityAnnotation> labels = await clientGoogle.DetectLabelsAsync(Image.FromBytes(ImageConverter.Base64ToByte(imageFile.Replace($"data:image/{extension};base64,", ""))));
            return labels.Where(l => l.Score * 100 >= 70).ToList();

        }

        public void GetWeb(string imageFile, string extension)
        {
            WebDetection webDetection = clientGoogle.DetectWebInformation(Image.FromBytes(ImageConverter.Base64ToByte(imageFile.Replace($"data:image/{extension};base64,", ""))));
            foreach (WebDetection.Types.WebEntity entity in webDetection.WebEntities)
            {
                Console.WriteLine($"Web entity: {entity.EntityId} / {entity.Description} ({entity.Score})");
            }
        }

        /// <summary>
        /// Get all landmarks for this picture
        /// </summary>
        /// <param name="imageFile">Picture</param>
        /// <param name="extension">Extension picture</param>
        /// <returns>All landmarks with over 70% of prediction</returns>
        public async Task<List<StepValidation>> GetWhat(string imageFile, string extension, int idStep)
        {
            IReadOnlyList<EntityAnnotation> landsmarks = await clientGoogle.DetectLandmarksAsync(Image.FromBytes(ImageConverter.Base64ToByte(imageFile.Replace($"data:image/{extension};base64,", ""))));
            return landsmarks.Where(l => l.Score * 100 >= 70).Select(l => new StepValidation { Id = 0, IdStep = idStep, Description = l.Description, Score = l.Score }).ToList();
        }
    }
}