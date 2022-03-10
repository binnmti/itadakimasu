using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
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
        private static readonly string TrainingEndPoint = "https://itadakimasugallery.cognitiveservices.azure.com/";
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

        public ImageCreateSummary CreateTrainingImages(string foodName, List<string> imageUrlList)
            => TrainingClient.CreateImagesFromUrls(ProjectGuid, new ImageUrlCreateBatch
            {
                TagIds = new List<Guid>() { GetTag(foodName).Id },
                Images = imageUrlList.Take(ImageUrlsLimited).Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
            });


        public string TestIteration(string url)
        {
            var sb = new StringBuilder();
            string PublishedModelName = "Iteration3";
            var result = PredictionClient.ClassifyImageUrl(ProjectGuid, PublishedModelName, new CustomVisionPrediction.Models.ImageUrl(url));
            result.Predictions.Take(5).ToList().ForEach(c => sb.AppendLine($"{c.TagName}:{c.Probability * 100}%"));
            return sb.ToString();
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
