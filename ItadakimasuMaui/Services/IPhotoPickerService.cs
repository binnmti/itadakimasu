namespace ItadakimasuMaui.Services;

public interface IPhotoPickerService
{
    Task<Stream> GetImageStreamAsync();
}
