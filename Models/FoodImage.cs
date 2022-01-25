using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FoodImage
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int Bit { get; set; }
        public int Size { get; set; }
        public string BlobUrl { get; set; } = "";
        public string BaseUrl { get; set; } = "";
    }
}
