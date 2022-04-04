using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System.IO;
using System.Threading.Tasks;

namespace ItadakimasuMobile.Utils
{
    public static class ImageSharpAdapter
    {
        public static async Task<(double, double)> GetGpsAsync(Stream stream)
        {
            double GetDecimalNumber(Rational[] rationals)
               => rationals[0].Numerator + ((double)rationals[1].Numerator / 60) + (rationals[2].Numerator / (double)rationals[2].Denominator / 3600);

            using (var image = await Image.LoadAsync(stream))
            {
                var latiude = image.Metadata.ExifProfile.GetValue(ExifTag.GPSLatitude);
                var longitude = image.Metadata.ExifProfile.GetValue(ExifTag.GPSLongitude);
                if (latiude != null && longitude != null)
                {
                    return (GetDecimalNumber(latiude.Value), GetDecimalNumber(longitude.Value));
                }
                else
                {
                    return (0, 0);
                }
            }
        }
    }
}

