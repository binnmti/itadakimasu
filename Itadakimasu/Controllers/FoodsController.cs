using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("food-list")]
        public async Task<ActionResult<List<Food>>> FoodList()
            => await _context.Food.ToListAsync();

        [HttpGet("{name}")]
        public async Task<ActionResult<Food>> GetFood(string name)
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

            var food = new Food() { Name = name, FoodImageCount = _context.FoodImage.Count(x => x.FoodName == name) };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }

        private async Task<Food?> FindAsync(string name)
            => await _context.Food.SingleOrDefaultAsync(x => x.Name == name);

    }
}
