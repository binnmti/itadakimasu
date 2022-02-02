using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public record Food
{
    public long Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
}
