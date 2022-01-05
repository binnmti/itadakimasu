using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using itadakimasu.Models;

namespace itadakimasu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly ItadakimasuContext _context;
        private readonly IConfiguration _configuration;

        public FoodsController(ItadakimasuContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(string name)
        {
            //認識できない画像の場合は不正チェックにもなるので時間がかかっても最初にやる
            if (string.IsNullOrEmpty(name)) return BadRequest();
           
            BingSearchUtility.GetContentUrlList(new HttpClient(), name, _configuration.GetConnectionString("BingCustomSearchSubscriptionKey"), _configuration.GetConnectionString("BingCustomSearchCustomConfigId"));

            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
        }


        //[HttpPost]
        //public async Task<ActionResult<Food>> PostFood(string name, string url)
        //{
        //    //認識できない画像の場合は不正チェックにもなるので時間がかかっても最初にやる
        //    if (string.IsNullOrEmpty(url))
        //    {
        //    }
        //    var food = await _context.Food.FindAsync(name);
        //    if (food == null)
        //    {
        //        food = new Food() { Name = name };
        //        _context.Food.Add(food);
        //        await _context.SaveChangesAsync();
        //    }
        //    return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
        //}





        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFood()
        {
            return await _context.Food.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(long id)
        {
            var food = await _context.Food.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        //// POST: api/Foods
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Food>> PostFood(Food food)
        //{
        //    _context.Food.Add(food);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
        //}

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(long id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.Food.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodExists(long id)
        {
            return _context.Food.Any(e => e.Id == id);
        }
    }
}
