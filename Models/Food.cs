using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Food
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; } = "";
    }
}
