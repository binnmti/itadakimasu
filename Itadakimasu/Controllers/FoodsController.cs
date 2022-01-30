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

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(string name)
        {
            if (_context.Food.Any(x => x.Name == name)) return Conflict();

            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }
    }
}
