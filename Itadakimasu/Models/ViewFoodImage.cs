using Models;

namespace Itadakimasu.Models
{
    public record ViewFoodImage(bool Checked, long Id, string Name, string FoodName, string XY, string Size, string BlobUrl, string BlobSUrl, string BaseUrl, int StatusNumber, string TestResult, long PrevId, long NextId);

    internal static class VewFoodImageConvert
    {
        internal static IEnumerable<ViewFoodImage> ToViewFoodImages(this List<FoodImage> foodImages)
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
                    x.TestResult,
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
    }
}
