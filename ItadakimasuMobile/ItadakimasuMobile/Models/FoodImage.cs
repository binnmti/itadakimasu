using System.Collections.Generic;

namespace ItadakimasuMobile.Models
{
    public class FoodImageResult
    {
        public string FoodName { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public List<Shop> Shops { get; set; }
    }

    public class Shop
    {
        public string FoodName { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

    }
}


