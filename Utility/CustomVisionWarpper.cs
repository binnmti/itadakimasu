﻿using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace Utility
{
    public class CustomVisionWarpper
    {
        private static readonly int ImageUrlsLimited = 64;
        private static readonly string TrainingEndPoint = "https://itadakimasugallery.cognitiveservices.azure.com/";
        private static readonly string PredictionEndpoint = "https://itadakimasugallery.cognitiveservices.azure.com/";
        
        private CustomVisionTrainingClient TrainingClient { get; }
        private CustomVisionPredictionClient PredictionClient { get; }
        
        private Guid ProjectGuid { get; }

        public CustomVisionWarpper(HttpClient httpclient, string trainingKey, string projectId)
        {
            TrainingClient = GetTrainingClient(httpclient, trainingKey);
            //PredictionClient = GetPredictionClient(trainingKey);
            ProjectGuid = TrainingClient.GetProject(new Guid(projectId)).Id;
        }

        public void CreateTrainingImages(string foodName, List<string> imageUrlList)
            => TrainingClient.CreateImagesFromUrls(ProjectGuid, new ImageUrlCreateBatch
            {
                TagIds = new List<Guid>() { GetTag(foodName).Id },
                Images = imageUrlList.Take(ImageUrlsLimited).Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
            });

        public void Test()
        {
            //var result = PredictionClient.ClassifyImageUrl(ProjectGuid, , , publishedModelName, testImage);
            //foreach (var c in result.Predictions)
            //{
            //    Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
            //}

        }

        private CustomVisionTrainingClient GetTrainingClient(HttpClient httpclient, string trainingKey)
            => new(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey), httpclient, false)
            {
                Endpoint = TrainingEndPoint
            };

        private CustomVisionPredictionClient GetPredictionClient(string predictionKey)
            => new(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = PredictionEndpoint
            };

        private Tag GetTag(string foodName)
            => TrainingClient.GetTags(ProjectGuid).SingleOrDefault(x => x.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
                ?? TrainingClient.CreateTag(ProjectGuid, foodName);
    }
}