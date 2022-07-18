using System.ComponentModel.DataAnnotations;

namespace Itadakimasu;

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

public record FoodImageResult(string FoodName, double Lat, double Lng, List<Shop> Shops);
public record Shop(string Name, string Address, double Lat, double Lng);


