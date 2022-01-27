using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Utility;

public static class ImageSharpAdapter
{
    public record ImageInfo(Stream Base, Stream Thumbnail, int Width, int Height);

    public static ImageInfo GetImageInfo(Stream stream, int width, int height)
    {
        using var image = Image.Load(stream);
        var w = image.Width; 
        var h = image.Height;
        var scale = Math.Min((float)width / w, (float)height / h);
        var baseStream = GetStream(image, 100);
        image.Mutate(x => x.Resize((int)(image.Width * scale), (int)(image.Height * scale)));
        var thumbnailStream = GetStream(image, 30);
        return new ImageInfo(baseStream, thumbnailStream, w, h);
    }

    private static Stream GetStream(Image image, int quality)
    {
        var output = new MemoryStream();
        image.Save(output, new JpegEncoder() { Quality = quality });
        output.Position = 0;
        return output;
    }
}
