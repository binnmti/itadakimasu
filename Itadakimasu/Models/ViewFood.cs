using Models;

namespace Itadakimasu.Models
{
    public record ViewFood(string Name, int FoodImageCount);

    public static class VewFoodConvert
    {
        public static IEnumerable<ViewFood> ToViewFoods(this IEnumerable<Food> food)
            => food.Select(x => new ViewFood(x.Name, 0));
    }
}
