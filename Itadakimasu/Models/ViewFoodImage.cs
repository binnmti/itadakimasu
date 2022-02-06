using Models;

namespace Itadakimasu.Models
{
    public record ViewFoodImage(bool Checked, string Name, string FoodName, string XY, string Size, string BlobUrl, string BaseUrl);

    public static class VewFoodImageConvert
    {
        private static readonly string BlobUrl = "https://itadakimasu.blob.core.windows.net/foodimage";

        public static IEnumerable<ViewFoodImage> ToViewFoodImages(this IEnumerable<FoodImage> foodImages)
            => foodImages.Select(x => new ViewFoodImage(true, x.BlobName, x.FoodName, x.ToXY(), x.ToSize(), x.ToBlobUrl(), x.BaseUrl));

        private static string ToXY(this FoodImage foodImage)
            => $"{foodImage.BlobWidth}X{foodImage.BlobHeight}";

        private static string ToSize(this FoodImage foodImage)
        {
            if (foodImage.BlobSize < 1024) return $"{foodImage.BlobSize}B";
            else if (foodImage.BlobSize < 1024 * 1024) return $"{foodImage.BlobSize / 1024}KB";
            else if (foodImage.BlobSize < 1024 * 1024 * 1024) return $"{foodImage.BlobSize / 1024 / 1024}MB";
            else return $"{foodImage.BlobSize / 1024 / 1024 / 1024}GB";
        }

        private static string ToBlobUrl(this FoodImage foodImage) => $"{BlobUrl}/{foodImage.FoodName}/{foodImage.BlobSName}";
    }
}
