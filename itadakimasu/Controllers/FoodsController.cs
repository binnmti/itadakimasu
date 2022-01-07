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
        public async Task<ActionResult<Food>> Food(string name)
        {
            //認識できない画像の場合は不正チェックにもなるので時間がかかっても最初にやる
            if (string.IsNullOrEmpty(name)) return BadRequest();
            var htmlClient = new HttpClient();
            var urlList = BingSearchUtility.GetContentUrlList(htmlClient, name, _configuration.GetConnectionString("BingCustomSearchSubscriptionKey"), _configuration.GetConnectionString("BingCustomSearchCustomConfigId"));
            CustomVisionUtility.Upload(_configuration.GetConnectionString("CustomVisionTrainingKey"), _configuration.GetConnectionString("CustomVisionProjectId"), name, urlList);
            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Food), new { id = food.Id }, food);
        }
    }
}
