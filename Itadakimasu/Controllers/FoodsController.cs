using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Utility;
using Microsoft.Extensions.Options;

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

        [HttpGet("get-food-name")]
        public string GetFoodName(string imageUrl)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var customVisionWarpper = new CustomVisionWarpper(httpClient, ConnectionStrings.CustomVisionTrainingKey, ConnectionStrings.CustomVisionpPredictionKey, ConnectionStrings.CustomVisionProjectId);

            //TODO:第一引数は戻し方なので再設計
            var result = customVisionWarpper.TestIteration("", imageUrl);
            return result;
        }
    }
}
