﻿using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using System.Text;
using CustomVisionPrediction = Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using CustomVisionTraining = Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;

namespace Utility
{
    public class CustomVisionWarpper
    {
        private static readonly int ImageUrlsLimited = 64;
        private static readonly string TrainingEndPoint = "https://itadakimasu.cognitiveservices.azure.com/";
        private static readonly string PredictionEndpoint = "https://itadakimasu-prediction.cognitiveservices.azure.com/";
        
        private CustomVisionTrainingClient TrainingClient { get; }
        private CustomVisionPredictionClient PredictionClient { get; }
        
        private Guid ProjectGuid { get; }

        public CustomVisionWarpper(HttpClient httpclient, string trainingKey, string predictionKey, string projectId)
        {
            TrainingClient = GetTrainingClient(httpclient, trainingKey);
            PredictionClient = GetPredictionClient(httpclient, predictionKey);
            ProjectGuid = TrainingClient.GetProject(new Guid(projectId)).Id;
        }

        public IEnumerable<ImageCreateSummary> CreateTrainingImages(string foodName, List<string> imageUrlList)
            => imageUrlList.Chunk(ImageUrlsLimited).Select(x => TrainingClient.CreateImagesFromUrls(ProjectGuid, new ImageUrlCreateBatch
            {
                TagIds = new List<Guid>() { GetTag(foodName).Id },
                Images = x.Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
            }));

        public string TestIteration(string foodName, string url)
        {
            string PublishedModelName = "Iteration3";
            var result = PredictionClient.ClassifyImageUrl(ProjectGuid, PublishedModelName, new CustomVisionPrediction.Models.ImageUrl(url));
            var model = result.Predictions.First();
            return foodName == model.TagName ? "" : $"{model.TagName}:{model.Probability:P1}";
        }

        private static CustomVisionTrainingClient GetTrainingClient(HttpClient httpclient, string trainingKey)
            => new(new CustomVisionTraining.ApiKeyServiceClientCredentials(trainingKey), httpclient, false)
            {
                Endpoint = TrainingEndPoint
            };

        private static CustomVisionPredictionClient GetPredictionClient(HttpClient httpclient, string predictionKey)
            => new(new CustomVisionPrediction.ApiKeyServiceClientCredentials(predictionKey), httpclient, false)
            {
                Endpoint = PredictionEndpoint
            };

        private Tag GetTag(string foodName)
            => TrainingClient.GetTags(ProjectGuid).SingleOrDefault(x => x.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
                ?? TrainingClient.CreateTag(ProjectGuid, foodName);
    }
}
