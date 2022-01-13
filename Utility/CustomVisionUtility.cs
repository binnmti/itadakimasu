using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace Utility
{
    public static class CustomVisionUtility
    {
        private static readonly int ImageUrlsLimited = 64;
        private static readonly string EndPoint = "https://itadakimasugallery.cognitiveservices.azure.com/";

        public async static Task UploadAsync(HttpClient httpClient, string trainingKey, string projectId, string foodName, List<string> imageUrlList)
        {
            var client = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey), httpClient, false)
            {
                Endpoint = EndPoint
            };
            var guid = (await client.GetProjectAsync(new Guid(projectId))).Id;
            var tag = await client.GetTagAsync(guid, foodName);


            var result = client.CreateImagesFromUrls(guid, new ImageUrlCreateBatch
            {
                TagIds = new List<Guid>() { tag.Id },
                Images = imageUrlList.Take(ImageUrlsLimited).Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
            });
        }

        //public async static Task TrainProject(string trainingKey, string projectId, string foodName, List<string> imageUrlList)
        //{
        //    var client = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(trainingKey))
        //    {
        //        Endpoint = EndPoint
        //    };
        //    //var guid = (await client.GetProjectAsync(new Guid(projectId))).Id;
        //    //client.ClassifyImageAsync()

        //}

        private static async Task<Tag> GetTagAsync(this CustomVisionTrainingClient client, Guid guid, string foodName)
            => (await client.GetTagsAsync(guid)).SingleOrDefault(x => x.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
                ?? await client.CreateTagAsync(guid, foodName);
    }
}
