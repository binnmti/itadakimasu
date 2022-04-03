using System.ComponentModel.DataAnnotations;

namespace Models;

public record MyFoodImage
{
    public long Id { get; set; }

    public string UserName { get; set; } = "";

    [MaxLength(100)]
    public string FoodName { get; set; } = "";

    public string EatingPlace { get; set; } = "";
    public double Lat { get; set; }
    public double Lng { get; set; }

    public string Memo { get; set; } = "";
}

