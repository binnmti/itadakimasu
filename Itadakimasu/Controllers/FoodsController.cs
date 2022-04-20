using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Utility;
using Microsoft.Extensions.Options;
using Models;
using ModelsShop = Models.Shop;

namespace Itadakimasu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly ItadakimasuContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConnectionStrings ConnectionStrings;

        public FoodsController(ItadakimasuContext context, IHttpClientFactory httpClientFactory, IOptions<ConnectionStrings> connectionStrings)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            ConnectionStrings = connectionStrings.Value;
        }

        //[HttpGet("get-food-name")]
        //public string GetFoodName(string imageUrl)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var customVisionWarpper = new CustomVisionWarpper(httpClient, ConnectionStrings.CustomVisionTrainingKey, ConnectionStrings.CustomVisionpPredictionKey, ConnectionStrings.CustomVisionProjectId);

        //    //TODO:第一引数は戻し方なので再設計
        //    var result = customVisionWarpper.TestIteration("", imageUrl);
        //    return result;
        //}

        [HttpPost("get-food-name")]
        public async Task<string> GetFoodName()
        {
            using var stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var httpClient = _httpClientFactory.CreateClient();
            var customVisionWarpper = new CustomVisionWarpper(httpClient, ConnectionStrings.CustomVisionTrainingKey, ConnectionStrings.CustomVisionpPredictionKey, ConnectionStrings.CustomVisionProjectId);
            return customVisionWarpper.TestIteration("", stream);
        }

        [HttpPost("get-food-gps")]
        public async Task<(double, double)> GetFoodGps()
        {
            using var stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return ImageSharpAdapter.GetGps(stream);
        }



        [HttpPost("get-food-image-result")]
        public async Task<FoodImageResult> GetFoodImageResult()
        {
            using var stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var httpClient = _httpClientFactory.CreateClient();
            var customVisionWarpper = new CustomVisionWarpper(httpClient, ConnectionStrings.CustomVisionTrainingKey, ConnectionStrings.CustomVisionpPredictionKey, ConnectionStrings.CustomVisionProjectId);
            var (lat, lng) = ImageSharpAdapter.GetGps(stream);
            var hotpepper = new HotpepperAdapter(ConnectionStrings.HotpepperKey, httpClient);
            var shops = await hotpepper.GetResultAsync(lat, lng);
            stream.Seek(0, SeekOrigin.Begin);
            //TODO:第一引数は戻し方なので再設計
            var foodName = customVisionWarpper.TestIteration("", stream);
            return new FoodImageResult(foodName, lat, lng, shops.Select(x => new ModelsShop(x.Name, x.Address, x.Lat, x.Lng)).ToList());
        }
    }
}
