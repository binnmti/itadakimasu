using Models;

namespace Itadakimasu.Models
{
    public record ViewFoodImage(bool Checked, string Name, int X, int Y, long Size, string BlobUrl, string BaseUrl);

    public static class VewFoodImageConvert
    {
        public static IEnumerable<ViewFoodImage> ToViewFoodImages(this IEnumerable<FoodImage> foodImages)
            => foodImages.Select(x => new ViewFoodImage(true, x.FoodName, x.BlobWidth, x.BlobHeight, x.BlobSize, x.BlobUrl, x.BaseUrl));
    }
}
