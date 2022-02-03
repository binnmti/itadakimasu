using System.ComponentModel.DataAnnotations;

namespace Models;
public record Food
{
    public long Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
    public int FoodImageCount { get; set; } 
}
