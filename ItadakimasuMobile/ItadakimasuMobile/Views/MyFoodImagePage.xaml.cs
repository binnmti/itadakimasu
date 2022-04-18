using ItadakimasuMobile.Services;
using ItadakimasuMobile.Utils;
using ItadakimasuMobile.ViewModels;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace ItadakimasuMobile.Views
{
    public partial class MyFoodImagePage : ContentPage
    {
        MyFoodImageViewModel _viewModel;

        public MyFoodImagePage()
        {
            InitializeComponent();

            var hotpepperApiKey = UserSecretsManager.Settings["HotpepperApiKey"];
            BindingContext = _viewModel = new MyFoodImageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            var httpClient = new HttpClient();
            var hotpepperApiKey = UserSecretsManager.Settings["HotpepperApiKey"];
            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                _viewModel.FoodImage = stream;
                //var imageSource = ImageSource.FromStream(() => stream);
                //FoodImage.Source = imageSource;

                //(_viewModel.Lat, _viewModel.Lng) = await ImageSharpAdapter.GetGpsAsync(stream);
                //var hotpepper = new HotpepperAdapter(hotpepperApiKey, httpClient);
                //var result = await hotpepper.GetResultAsync(_viewModel.Lat.ToString(), _viewModel.Lng.ToString());
            }

            (sender as Button).IsEnabled = true;
        }
    }
}