namespace Itadakimasu.Models
{
    public record ViewFoodViewer(IEnumerable<ViewFood> Foods, PaginatedList<ViewFoodImage> FoodImages);
}
