#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Itadakimasu.Data;
using Models;

namespace Itadakimasu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly ItadakimasuContext _context;

        public FoodsController(ItadakimasuContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(string name)
        {
            if (_context.Food.Any(x => x.Name == name)) return BadRequest();

            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }

        [HttpPost]
        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            if (_context.FoodImage.Any(x => x.BaseUrl == foodImage.BaseUrl)) return BadRequest();

            _context.FoodImage.Add(foodImage);
            await _context.SaveChangesAsync();
            return foodImage;
        }

    }
}
