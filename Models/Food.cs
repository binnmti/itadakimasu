using System.ComponentModel.DataAnnotations;

namespace Models;
public record Food
{
    public long Id { get; set; }
    [MaxLength(500)]
    public string Name { get; set; } = "";
}
