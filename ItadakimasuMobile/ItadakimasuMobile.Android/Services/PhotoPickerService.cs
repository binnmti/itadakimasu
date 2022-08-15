using Android.Content;
using ItadakimasuMobile.Droid.Services;
using ItadakimasuMobile.Services;
using Java.IO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace ItadakimasuMobile.Droid.Services
{
    public class PhotoPickerService : IPhotoPickerService
    {
        //private static void ListFiles(List<byte[]> streamList, Java.IO.File file)
        //{
        //    var files = file.ListFiles();
        //    if (files == null) return;

        //    foreach(var f in files)
        //    {
        //        if (f.IsDirectory)
        //        {
        //            //var bytes = new byte[f.Length()];
        //            //var fos = new FileInputStream(f);
        //            //fos.Read(bytes);
        //            //streamList.Add(bytes);
        //            ListFiles(streamList, f);
        //        }
        //    }
        //}

        public Task<Stream> GetImageStreamAsync()
        {
            //string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryPictures);
            //var byteList = new List<byte[]>();
            //ListFiles(byteList, new Java.IO.File(directory));

            var intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Select Photo"), MainActivity.PickImageId);
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}