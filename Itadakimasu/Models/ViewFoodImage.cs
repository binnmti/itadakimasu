namespace Itadakimasu.Models
{
    public record ViewFoodImage
    {
        public bool Checked { get; set; }
        public string Name { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int Bit { get; set; }
        public int Size { get; set; }
        public string BlobUrl { get; set; } = "";
        public string BaseUrl { get; set; } = "";
    }
}
