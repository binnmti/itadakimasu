using ItadakimasuMobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ItadakimasuMobile.ViewModels
{
    public class MyFoodImageViewModel : BaseViewModel
    {
        public HttpClient HttpClient { get; } = new HttpClient();

        public MyFoodImageViewModel()
        {
            Title = "MyFoodImage";
            //AuthenticateCommand = new Command(() => { });
        }

        public async Task SetStreamAsync(Stream stream)
        {
            if (stream == null) return;

            HttpResponseMessage message;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                stream.Seek(0, SeekOrigin.Begin);
                var content = new ByteArrayContent(ms.ToArray());
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                message = await HttpClient.PostAsync("https://localhost:7162/api/foods/get-food-image-result", content);
                //message = await HttpClient.PostAsync("https://itadakimasu.azurewebsites.net/api/foods/get-food-image-result", content);
            }
            if (!message.IsSuccessStatusCode) return;

            var json = await message.Content.ReadAsStringAsync();
            var foodImageResult = JsonConvert.DeserializeObject<FoodImageResult>(json);
            FoodImage = stream;
            FoodName = foodImageResult.FoodName;
            Lat = foodImageResult.Lat;
            Lng = foodImageResult.Lng;
            if (foodImageResult.Shops.Count > 0)
            {
                Shops = foodImageResult.Shops.Select(x => x.FoodName).ToList();
            }
        }

        private Stream foodImage;
        public Stream FoodImage
        {
            get => foodImage;
            set
            {
                foodImage = value;
                SetProperty(ref foodImage, value);
            }
        }

        private string foodName;
        public string FoodName
        {
            get => foodName;
            set => SetProperty(ref foodName, value);
        }

        private string eatingPlace;
        public string EatingPlace
        {
            get => eatingPlace;
            set => SetProperty(ref eatingPlace, value);
        }

        private double lat;
        public double Lat
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }

        private double lng;
        public double Lng
        {
            get => lng;
            set => SetProperty(ref lng, value);
        }

        private List<string> shops;
        public List<string> Shops
        {
            get => shops;
            set => SetProperty(ref shops, value);
        }

        private string memo;
        public string Memo
        {
            get => memo;
            set => SetProperty(ref memo, value);
        }
    }
}
