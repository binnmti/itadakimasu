using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;

namespace Utility;

public static class ImageSharpAdapter
{
    //サイズは結構小さくなるが、クオリティが担保出来ると判断した圧縮率
    private const int JpegEncoderQuality = 30;
    public record Jpeg(Stream Image, int Width, int Height, Stream ThumbnailImage, int ThumbnailWidth, int ThumbnailHeight);

    public static Jpeg ConvertJpeg(Stream stream, int thumbnailWidth, int thumbnailHeight)
    {
        using var image = Image.Load(stream);
        var width = image.Width; 
        var height = image.Height;
        var scale = Math.Min((float)thumbnailWidth / width, (float)thumbnailHeight / height);
        var imageStream = GetJpegStream(image, 100);
        image.Mutate(x => x.Resize((int)(image.Width * scale), (int)(image.Height * scale)));
        var thumbnailStream = GetJpegStream(image, JpegEncoderQuality);
        return new Jpeg(imageStream, width, height, thumbnailStream, (int)(image.Width * scale), (int)(image.Height * scale));
    }

    public static (double, double) GetGps(string fs)
    {
        using var image = Image.Load(fs);
        var latiude = image.Metadata.ExifProfile.GetValue(ExifTag.GPSLatitude);
        var longitude = image.Metadata.ExifProfile.GetValue(ExifTag.GPSLongitude);
        if(latiude != null && longitude != null)
        {
            return (GetDecimalNumber(latiude.Value), GetDecimalNumber(longitude.Value));
        }
        else
        {
            return (0, 0);
        }
    }

    private static double GetDecimalNumber(Rational[] rationals)
        => rationals[0].Numerator + ((double)rationals[1].Numerator / 60) + (rationals[2].Numerator / (double)rationals[2].Denominator / 3600);

    private static Stream GetJpegStream(Image image, int quality)
    {
        var output = new MemoryStream();
        image.Save(output, new JpegEncoder() { Quality = quality });
        output.Position = 0;
        return output;
    }
}
