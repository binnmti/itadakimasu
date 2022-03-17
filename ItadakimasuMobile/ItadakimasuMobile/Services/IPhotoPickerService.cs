using System.IO;
using System.Threading.Tasks;

namespace ItadakimasuMobile.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
