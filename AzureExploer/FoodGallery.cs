namespace AzureExploer
{
    internal class FoodImage
    {
        internal bool Checked { get; set; }
        internal string Url { get; set; } = "";
        internal Bitmap Bitmap { get; set; } = null!;
    }
    internal class FoodGallery
    {
        internal string Name { get; set; } = "";
        internal List<FoodImage> FoodImages { get; set; } = null!;
    }

}
