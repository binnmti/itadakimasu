using Itadakimasu.Controllers;
using Models;

namespace Itadakimasu.Models
{
    public record ViewFood(string Name, int FoodImageCount, string BlobUrl);

    public static class VewFoodConvert
    {
        public static IEnumerable<ViewFood> ToViewFoods(this List<FoodImagesController.Food> food)
            => food.Select(x => new ViewFood(x.Name, x.FoodImageCount, x.First.ToBlobUrl()));
    }
}
