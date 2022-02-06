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
        public async Task<IActionResult> FoodViewer(string foodName, string size = "")
        {
            if (string.IsNullOrEmpty(foodName)) foodName = "オムライス";

            var ViewerSize = new ViewerSize();
            ViewerSize.SaveCookie(size, Request.Cookies, Response.Cookies);
            ViewData["ViewerSize"] = ViewerSize;

            var client = _clientFactory.CreateClient();
            var foods = await client.GetFromJsonAsync<List<Food>>($"{Request.Scheme}://{Request.Host}/api/foods/food-list") ?? new List<Food>();
            var foodImages = await client.GetFromJsonAsync<List<FoodImage>>($"{Request.Scheme}://{Request.Host}/api/foodimages/food-image-list/{foodName}") ?? new List<FoodImage>();
            return View(new ViewFoodViewer(foods.ToViewFoods(), foodImages.ToViewFoodImages()));
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