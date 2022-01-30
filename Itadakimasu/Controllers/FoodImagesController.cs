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
            var foodImage = await _context.FoodImage.FindAsync(baseUrl);
            if (foodImage == null)
            {
                return NotFound();
            }
            return foodImage;
        }

        [HttpPost]
        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            if (_context.FoodImage.Any(x => x.BaseUrl == foodImage.BaseUrl)) return Conflict();

            _context.FoodImage.Add(foodImage);
            await _context.SaveChangesAsync();
            return foodImage;
        }
    }
}
