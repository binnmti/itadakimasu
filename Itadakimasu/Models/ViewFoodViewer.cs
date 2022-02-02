namespace Itadakimasu.Models
{
    public record ViewFoodViewer(IEnumerable<ViewFood> Foods, IEnumerable<ViewFoodImage> FoodImages);
}
