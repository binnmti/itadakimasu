namespace ItadakimasuWeb.Models
{
    public record ViewFoodViewer(IEnumerable<ViewFood> Foods, PaginatedList<ViewFoodImage> FoodImages);
}
