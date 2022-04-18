using ItadakimasuMobile.Services;
using ItadakimasuMobile.Utils;
using Xamarin.Forms;

namespace ItadakimasuMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
