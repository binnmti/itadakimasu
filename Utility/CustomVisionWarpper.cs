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
        private static readonly string TrainingEndPoint = "https://itadakimasu.cognitiveservices.azure.com/";
        private static readonly string PredictionEndpoint = "https://itadakimasu-prediction.cognitiveservices.azure.com/";
        private static readonly string PublishedModelName = "Iteration1";

        private CustomVisionTrainingClient TrainingClient { get; }
        private CustomVisionPredictionClient PredictionClient { get; }
        
        private Guid ProjectGuid { get; }

        public CustomVisionWarpper(HttpClient httpclient, string trainingKey, string predictionKey, string projectId)
        {
            TrainingClient = GetTrainingClient(httpclient, trainingKey);
            PredictionClient = GetPredictionClient(httpclient, predictionKey);
            ProjectGuid = TrainingClient.GetProject(new Guid(projectId)).Id;
        }

        public List<ImageCreateSummary> CreateTrainingImages(string foodName, List<string> imageUrlList)
            => imageUrlList.Chunk(ImageUrlsLimited).Select(x => TrainingClient.CreateImagesFromUrls(ProjectGuid, new ImageUrlCreateBatch
            {
                TagIds = new List<Guid>() { GetTag(foodName).Id },
                Images = x.Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
            })).ToList();

        public string TestIteration(string foodName, string url)
        {
            var result = PredictionClient.ClassifyImageUrl(ProjectGuid, PublishedModelName, new CustomVisionPrediction.Models.ImageUrl(url));
            var model = result.Predictions.First();
            return foodName == model.TagName ? "" : $"{model.TagName}:{model.Probability:P1}";
        }

        public string TestIteration(string foodName, Stream stream)
        {
            var result = PredictionClient.ClassifyImage(ProjectGuid, PublishedModelName, stream);
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
