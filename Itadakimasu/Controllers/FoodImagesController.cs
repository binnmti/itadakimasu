using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Models;

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

        [HttpGet("{baseUrl}")]
        public async Task<ActionResult<FoodImage>> GetFoodImage(string baseUrl)
        {
            var foodImage = await Find(baseUrl);
            if (foodImage == null) return NotFound();
            return foodImage;
        }

        [HttpPost]
        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            var hit = await Find(foodImage.BaseUrl);
            if (hit != null) return Conflict();

            _context.FoodImage.Add(foodImage);
            await _context.SaveChangesAsync();
            return foodImage;
        }

        private async Task<FoodImage?> Find(string baseUrl)
            => await _context.FoodImage.FindAsync(baseUrl);

    }
}
