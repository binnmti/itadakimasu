using ItadakimasuMobile.Services;
using ItadakimasuMobile.ViewModels;
using System;
using Xamarin.Forms;

namespace ItadakimasuMobile.Views
{
    public partial class MyFoodImagePage : ContentPage
    {
        MyFoodImageViewModel _viewModel;

        public MyFoodImagePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MyFoodImageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            //var mySecret = UserSecretsManager.Settings["MySecret"];

            (sender as Button).IsEnabled = false;

            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                _viewModel.FoodImage = stream;
                var imageSource = ImageSource.FromStream(() => stream);
                FoodImage.Source = imageSource;

                //(_viewModel.Lat, _viewModel.Lng) = await ImageSharpAdapter.GetGpsAsync(stream);
                //var httpClient = new System.Net.Http.HttpClient();
                //var hotpepper = new HotpepperAdapter(hotpepperApiKey, HttpClient);
                //var result = await hotpepper.GetResultAsync(lat.ToString(), lng.ToString());
            }

             (sender as Button).IsEnabled = true;
        }
    }
}