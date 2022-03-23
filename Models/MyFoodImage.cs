using System.ComponentModel.DataAnnotations;

namespace Models;

public record MyFoodImage
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string FoodName { get; set; } = "";
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string EatPlace { get; set; } = "";
}

