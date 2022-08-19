using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace ItadakimasuMaui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode != PickImageId) return;

            var stream = (resultCode == Result.Ok && intent != null) ? ContentResolver.OpenInputStream(intent.Data) : null;
            PickImageTaskCompletionSource.SetResult(stream);
        }
    }
}