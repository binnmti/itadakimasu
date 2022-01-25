using ItadakimasuViewer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utility;

namespace ItadakimasuViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var blobConnectionString = _configuration.GetConnectionString("BlobConnectionString");
            var blobAdapter = new BlobAdapter(blobConnectionString);
            var foodImages = new List<ViewFoodImage>();
            foreach (var blob in blobAdapter.GetBlobs("foodimage").Where(x => x.Name.Contains("オムライス")))
            {
                //最初からフォルダを分けた方がこれがいらないかも。
                if (!blob.Name.Contains("_s")) continue;
                foodImages.Add(new ViewFoodImage() { Checked = true, BlobUrl = $"{blobAdapter.Url}foodimage/{blob.Name}" });
            }
            return View(foodImages);
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