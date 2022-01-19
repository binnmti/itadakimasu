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

        public void Upload(Stream stream, string containerName, string blobName)
            => BlobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName).Upload(stream, true);

        public Response<BlobDownloadResult> Download(string containerName, string blobName)
            => BlobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName).DownloadContent();

        public Pageable<BlobItem> GetBlobItems(string containerName)
            => BlobServiceClient.GetBlobContainerClient(containerName).GetBlobs();
    }
}
