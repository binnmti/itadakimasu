using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Itadakimasu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodImagesController : ControllerBase
    {
        private readonly ItadakimasuContext _context;

        public FoodImagesController(ItadakimasuContext context)
        {
            _context = context;
        }

        [HttpGet("food-image-count")]
        public int FoodImageCount() => _context.FoodImage.Count();

        [HttpGet("{baseUrl}")]
        public async Task<ActionResult<FoodImage>> GetFoodImage(string baseUrl)
        {
            var foodImage = await FindAsync(HttpUtility.UrlDecode(baseUrl));
            if (foodImage == null) return NotFound();
            return foodImage;
        }

        [HttpPost]
        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            var hit = await FindAsync(foodImage.BaseUrl);
            if (hit != null) return Conflict();

            var count = await _context.FoodImage.CountAsync();
            foodImage.BlobName = $"{count:0000}.jpg";
            foodImage.BlobSName = $"{count:0000}_s.jpg";
            _context.FoodImage.Add(foodImage);
            await _context.SaveChangesAsync();
            return foodImage;
        }

        private async Task<FoodImage?> FindAsync(string baseUrl)
            => await _context.FoodImage.SingleOrDefaultAsync(x => x.BaseUrl == baseUrl);
    }
}
