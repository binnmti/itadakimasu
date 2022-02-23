using Itadakimasu.Models;
using Itadakimasu.Models.Select;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace Itadakimasu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public IActionResult Index() => View();

        [HttpGet]
        [Route("/FoodViewer")]
        public async Task<IActionResult> FoodViewer(string foodName, int page = 0, string size = "", int stateNumber = 0)
        {
            ViewData["StateNumber"] = stateNumber;

            var viewerCount = new SelectViewerCount();
            viewerCount.SaveCookie(size, Request.Cookies, Response.Cookies);
            ViewData[nameof(SelectViewerCount)] = viewerCount;
            _ = int.TryParse(viewerCount.CurrentKey, out var count);

            var viewerSize = new SelectViewerSize();
            viewerSize.SaveCookie(size, Request.Cookies, Response.Cookies);
            ViewData[nameof(SelectViewerSize)] = viewerSize;
            ViewData["CurrentViewerSize"] = viewerSize.CurrentKey;

            var client = _clientFactory.CreateClient();
            var foodApiUrl = $"{Request.Scheme}://{Request.Host}/api";

            var foods = await client.GetFromJsonAsync<List<FoodImagesController.Food>>($"{foodApiUrl}/foodimages/food-list?stateNumber={stateNumber}") ?? new List<FoodImagesController.Food>();
            if (string.IsNullOrEmpty(foodName)) foodName = foods.First().Name;
            var foodImages = await client.GetFromJsonAsync<List<FoodImage>>($"{foodApiUrl}/foodimages/food-image-list/{foodName}?page={page}&count={count}&stateNumber={stateNumber}") ?? new List<FoodImage>();
            var foodImageCount = await client.GetFromJsonAsync<int>($"{foodApiUrl}/foodimages/food-image-list-count/{foodName}?stateNumber={stateNumber}");
            return View(new ViewFoodViewer(foods.ToViewFoods(), new PaginatedList<ViewFoodImage>(foodImages.ToViewFoodImages(), page, count, foodImageCount, $"/FoodViewer?foodName={foodName}&stateNumber={stateNumber}&")));
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}