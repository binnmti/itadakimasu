using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Utility
{
    public static class ImageSharpAdapter
    {
        public static byte[] ImageResize(Stream stream, int width, int height)
        {
            using var image = Image.Load(stream);
            float scale = Math.Min((float)width / (float)image.Width, (float)height / (float)image.Height);
            int widthToScale = (int)(image.Width * scale);
            int heightToScale = (int)(image.Height * scale);

            image.Mutate(x => x.Resize(widthToScale, heightToScale));
            using var output = new MemoryStream();
            image.Save(output, new JpegEncoder() { Quality = 30 });
            return output.GetBuffer();
        }
    }
}
