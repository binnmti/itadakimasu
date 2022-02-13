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

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/FoodViewer/{foodName}")]
        public async Task<IActionResult> FoodViewer(string foodName, int page = 0, string size = "")
        {
            if (string.IsNullOrEmpty(foodName)) foodName = "オムライス";

            var viewerCount = new SelectViewerCount();
            viewerCount.SaveCookie(size, Request.Cookies, Response.Cookies);
            ViewData[nameof(SelectViewerCount)] = viewerCount;
            int.TryParse(viewerCount.CurrentKey, out var count);

            var viewerSize = new SelectViewerSize();
            viewerSize.SaveCookie(size, Request.Cookies, Response.Cookies);
            ViewData[nameof(SelectViewerSize)] = viewerSize;
            ViewData["CurrentViewerSize"] = viewerSize.CurrentKey;

            var client = _clientFactory.CreateClient();
            var foodApiUrl = $"{Request.Scheme}://{Request.Host}/api";
            var foods = await client.GetFromJsonAsync<List<Food>>($"{foodApiUrl}/foods/food-list") ?? new List<Food>();
            var foodImages = await client.GetFromJsonAsync<List<FoodImage>>($"{foodApiUrl}/foodimages/food-image-list/{foodName}?page={page}&count={count}") ?? new List<FoodImage>();
            var foodImageCount = await client.GetFromJsonAsync<int>($"{foodApiUrl}/foodimages/food-image-list-count/{foodName}");
            var viewFoodImages = foodImages.ToViewFoodImages().ToList();
            return View(new ViewFoodViewer(foods.ToViewFoods(viewFoodImages), new PaginatedList<ViewFoodImage>(viewFoodImages, page, count, foodImageCount, $"/FoodViewer/{foodName}?")));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}