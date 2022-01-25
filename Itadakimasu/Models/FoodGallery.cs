namespace Itadakimasu.Model
{
    public class BlobViewer
    {
        public List<FoodImage> FoodImages { get; set; } = new();
    }

    public class FoodImage
    {
        public bool Checked { get; set; }
        public string Url { get; set; } = "";
    }
}
