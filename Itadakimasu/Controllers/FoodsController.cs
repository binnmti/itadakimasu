using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{name}")]
        public async Task<ActionResult<FoodImage>> GetFood(string name)
        {
            var food = await FindAsync(name);
            if (food == null) return NotFound();
            return food;
        }


        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(string name)
        {
            var hit = await FindAsync(name);
            if (hit != null) return Conflict();

            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }

        private async Task<FoodImage?> FindAsync(string name)
            => await _context.FoodImage.FindAsync(name);

    }
}
