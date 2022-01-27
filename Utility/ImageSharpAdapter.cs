using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Utility;

public static class ImageSharpAdapter
{
    //サイズは結構小さくなるが、クオリティが担保出来ると判断した圧縮率
    private const int JpegEncoderQuality = 30;
    public record Jpeg(Stream Image, int Width, int Height, Stream ThumbnailImage);

    public static Jpeg ConvertJpeg(Stream stream, int thumbnailWidth, int thumbnailHeight)
    {
        using var image = Image.Load(stream);
        var width = image.Width; 
        var height = image.Height;
        var scale = Math.Min((float)thumbnailWidth / width, (float)thumbnailHeight / height);
        var imageStream = GetJpegStream(image, 100);
        image.Mutate(x => x.Resize((int)(image.Width * scale), (int)(image.Height * scale)));
        var thumbnailStream = GetJpegStream(image, JpegEncoderQuality);
        return new Jpeg(imageStream, width, height, thumbnailStream);
    }

    private static Stream GetJpegStream(Image image, int quality)
    {
        var output = new MemoryStream();
        image.Save(output, new JpegEncoder() { Quality = quality });
        output.Position = 0;
        return output;
    }
}
