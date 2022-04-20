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

            var httpClient = new HttpClient();
            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    var content = new ByteArrayContent(ms.ToArray());
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var result = await httpClient.PostAsync("https://localhost:7162/api/foods/get-food-image-result", content);
                    var json = await result.Content.ReadAsStringAsync();
                    var a = JsonConvert.DeserializeObject<FoodImageResult>(json);

                }



                //_viewModel.FoodImage = stream;
                //(_viewModel.Lat, _viewModel.Lng) = await ImageSharpAdapter.GetGpsAsync(stream);
                //var hotpepper = new HotpepperAdapter(hotpepperApiKey, httpClient);
                //var result = await hotpepper.GetResultAsync(_viewModel.Lat.ToString(), _viewModel.Lng.ToString());
            }

            (sender as Button).IsEnabled = true;
        }
    }
}