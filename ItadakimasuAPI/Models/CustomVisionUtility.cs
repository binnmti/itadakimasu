using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace ItadakimasuAPI.Models
{
    public static class CustomVisionUtility
    {
        private static readonly int ImageUrlsLimited = 64;
        private static readonly string EndPoint = "https://itadakimasugallery.cognitiveservices.azure.com/";

        public static void Upload(string trainingKey, string projectId, string foodName, List<string> imageUrlList)
        {
            var customVisionTrainingClient = new CustomVisionTrainingClient(new ApiKeyServiceClientCredentials(trainingKey))
            {
                Endpoint = EndPoint
            };
            var guid = customVisionTrainingClient.GetProject(new Guid(projectId)).Id;
            var tag = customVisionTrainingClient.GetTag(guid, foodName);
            imageUrlList.Chunk(ImageUrlsLimited).ToList().ForEach(x =>
            {
                customVisionTrainingClient.CreateImagesFromUrls(guid, new ImageUrlCreateBatch
                {
                    TagIds = new List<Guid>() { tag.Id },
                    Images = x.Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
                });
            });
        }

        private static Tag GetTag(this CustomVisionTrainingClient customVisionTrainingClient, Guid guid, string foodName)
            => customVisionTrainingClient.GetTags(guid).SingleOrDefault(x => x.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
                ?? customVisionTrainingClient.CreateTag(guid, foodName);
    }
}
