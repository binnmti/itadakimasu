using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Utility
{
    public class BlobAdapter
    {
        private BlobServiceClient BlobServiceClient { get; }
        public BlobAdapter(string blobConnectionString)
        {
            BlobServiceClient = new BlobServiceClient(blobConnectionString);
        }

        public string Url => BlobServiceClient.Uri.ToString();

        public void Upload(Stream stream, string containerName, string blobName)
            => BlobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName).Upload(stream, true);

        public Response<BlobDownloadResult> Download(string containerName, string blobName)
            => BlobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName).DownloadContent();

        public IEnumerable<string> GetBlobUrls(string containerName)
           => BlobServiceClient.GetBlobContainerClient(containerName)
                .GetBlobs().Select(x => $"{BlobServiceClient.Uri}{containerName}/{x.Name}");
    }
}
