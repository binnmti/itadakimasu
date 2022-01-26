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

        //https://localhost:7162/api/Foods
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(string name)
        {
            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        [HttpPost]
        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            _context.FoodImage.Add(foodImage);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFoodImage", new { id = foodImage.Id }, foodImage);
        }

    }
}
