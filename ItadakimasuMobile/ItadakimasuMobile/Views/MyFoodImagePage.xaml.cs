using ItadakimasuMobile.Services;
using ItadakimasuMobile.ViewModels;
using System;
using System.IO;
using Xamarin.Essentials;
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

        private async void OnSelectPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            await _viewModel.SetStreamAsync(stream);

            (sender as Button).IsEnabled = true;
        }

        private async void OnTakePhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            FileResult photo = default;
            try
            {
                photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null) return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var stream = await photo.OpenReadAsync();
            //TODO:Exifで常に縦になるようにしたい

            //TODO:この場合にExifに位置情報が入ってない。入れるにはどうすれば？
            //TODO:自分でGeoから位置情報とるのでは精度が違う？

            await _viewModel.SetStreamAsync(stream);
            File.Delete(photo.FullPath);

            (sender as Button).IsEnabled = true;
        }
    }
}