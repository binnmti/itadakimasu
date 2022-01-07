using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace itadakimasu.Models
{
    public class CustomVisionUtility
    {
        private const string EndPoint = "https://itadakimasugallery.cognitiveservices.azure.com/";
        private readonly Project Project;
        private readonly CustomVisionTrainingClient CustomVisionTrainingClient;

        public CustomVisionUtility(string trainingKey, string projectId)
        {
            CustomVisionTrainingClient = new(new ApiKeyServiceClientCredentials(trainingKey))
            {
                Endpoint = EndPoint
            };
            Project = CustomVisionTrainingClient.GetProject(new Guid(projectId));
        }

        public void Upload(HttpClient httpClient, string foodName, List<string> imageUrlList)
        {
            var tags = CustomVisionTrainingClient.GetTags(Project.Id);
            var tag = tags.SingleOrDefault(x => x.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase)) ?? CustomVisionTrainingClient.CreateTag(Project.Id, foodName);
            var Batch = new ImageUrlCreateBatch
            {
                TagIds = new List<Guid>() { tag.Id },
                Images = imageUrlList.Select(x => new ImageUrlCreateEntry() { Url = x }).ToList(),
            };
            CustomVisionTrainingClient.CreateImagesFromUrls(Project.Id, Batch);
        }
    }
}
