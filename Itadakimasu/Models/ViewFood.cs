using Models;

namespace Itadakimasu.Models
{
    public record ViewFood(string Name, int FoodImageCount, string BlobUrl);

    public static class VewFoodConvert
    {
        public static IEnumerable<ViewFood> ToViewFoods(this IEnumerable<Food> food, IEnumerable<ViewFoodImage> foodImage)
            => food.Select(x => new ViewFood(x.Name, x.FoodImageCount, foodImage.FirstOrDefault(i => i.FoodName == x.Name)?.BlobUrl ?? ""));
    }
}
