using Models;

namespace Itadakimasu.Models
{
    public record ViewFoodImage(bool Checked, string Name, int X, int Y, long Size, string BlobUrl, string BaseUrl);

    public static class VewFoodImageConvert
    {
        private static readonly string BlobUrl = "https://itadakimasu.blob.core.windows.net/food";
        public static IEnumerable<ViewFoodImage> ToViewFoodImages(this IEnumerable<FoodImage> foodImages)
            => foodImages.Select(x => new ViewFoodImage(true, x.FoodName, x.BlobWidth, x.BlobHeight, x.BlobSize, x.ToBlobUrl(), x.BaseUrl));

        //https://itadakimasu.blob.core.windows.net/food/寿司/Bing41775.jpg
        private static string ToBlobUrl(this FoodImage foodImage) => $"{BlobUrl}/{foodImage.FoodName}/{foodImage.BlobName}";
    }
}
