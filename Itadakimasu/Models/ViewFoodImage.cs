using Models;

namespace Itadakimasu.Models
{
    public record ViewFoodImage(bool Checked, long Id, string Name, string FoodName, string XY, string Size, string BlobUrl, string BlobSUrl, string BaseUrl, int StatusNumber, long PrevId, long NextId);

    public static class VewFoodImageConvert
    {
        private static readonly string BlobUrl = "https://itadakimasu.blob.core.windows.net/foodimage";

        public static IEnumerable<ViewFoodImage> ToViewFoodImages(this List<FoodImage> foodImages)
            => foodImages.Select((x, idx) => new ViewFoodImage(
                    true,
                    x.Id,
                    x.BlobName,
                    x.FoodName,
                    x.ToXY(),
                    x.ToSize(),
                    x.ToBlobUrl(),
                    x.ToBlobSUrl(),
                    x.BaseUrl,
                    x.StatusNumber,
                    idx == 0 ? foodImages.Last().Id : foodImages.ElementAt(idx - 1).Id,
                    idx == foodImages.Count - 1 ? foodImages.First().Id : foodImages.ElementAt(idx + 1).Id));

        private static string ToXY(this FoodImage foodImage)
            => $"{foodImage.BlobWidth}X{foodImage.BlobHeight}";

        private static string ToSize(this FoodImage foodImage)
        {
            if (foodImage.BlobSize < 1024) return $"{foodImage.BlobSize}B";
            else if (foodImage.BlobSize < 1024 * 1024) return $"{foodImage.BlobSize / 1024}KB";
            else if (foodImage.BlobSize < 1024 * 1024 * 1024) return $"{foodImage.BlobSize / 1024 / 1024}MB";
            else return $"{foodImage.BlobSize / 1024 / 1024 / 1024}GB";
        }

        private static string ToBlobUrl(this FoodImage foodImage) => $"{BlobUrl}/{foodImage.FoodName}/{foodImage.BlobName}";
        private static string ToBlobSUrl(this FoodImage foodImage) => $"{BlobUrl}/{foodImage.FoodName}/{foodImage.BlobSName}";
    }
}
