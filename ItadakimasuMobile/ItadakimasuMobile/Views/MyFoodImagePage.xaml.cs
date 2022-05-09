using ItadakimasuMobile.Models;
using ItadakimasuMobile.Services;
using ItadakimasuMobile.Utils;
using ItadakimasuMobile.ViewModels;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
            (sender as Button).IsEnabled = false;

            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                FoodImage.Source = ImageSource.FromStream(() => stream);
                stream.Seek(0, SeekOrigin.Begin);

                await _viewModel.SetStreamAsync(stream);
            }

            (sender as Button).IsEnabled = true;
        }
    }
}