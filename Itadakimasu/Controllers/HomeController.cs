using Itadakimasu.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Itadakimasu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult BlobViewer(string name)
        {
            var blobViewer = new BlobViewer();
            var blobConnectionString = _configuration.GetConnectionString("BlobConnectionString");
            var blobAdapter = new BlobAdapter(blobConnectionString);
            foreach (var blob in blobAdapter.GetBlobs("foodimage").Where(x => x.Name.Contains(name)))
            {
                //最初からフォルダを分けた方がこれがいらないかも。
                if (!blob.Name.Contains("_s")) continue;
                blobViewer.FoodImages.Add(new FoodImage() { Checked = true, Url = $"{blobAdapter.Url}foodimage/{blob.Name}" });
            }
            return View(blobViewer);
        }
    }
}
