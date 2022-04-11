using System.IO;
using Xamarin.Forms;

namespace ItadakimasuMobile.ViewModels
{
    public class MyFoodImageViewModel : BaseViewModel
    {
        public MyFoodImageViewModel()
        {
            Title = "MyFoodImage";
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

        private string memo;
        public string Memo
        {
            get => memo;
            set => SetProperty(ref memo, value);
        }
    }
}
