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

        [HttpGet("food-image-list-count/{foodName}")]
        public async Task<ActionResult<int>> FoodImageListCount(string foodName)
            => await _context.FoodImage.CountAsync(x => x.FoodName == foodName);

        [HttpGet("food-image-list/{foodName}")]
        public async Task<ActionResult<List<FoodImage>>> FoodImageList(string foodName, int page = 1, int count = 50)
        {
            page = Math.Max(page, 1);
            return await _context.FoodImage.Where(x => x.FoodName == foodName).Skip((page - 1) * count).Take(count).ToListAsync();
        }

        //新規追加名を返す
        [HttpGet("get-new-name")]
        public async Task<ActionResult<int>> GetNewName(string baseUrl, string searchAPI, string foodName)
        {
            var foodImage = await FindAsync(baseUrl);
            if (foodImage != null) return -1;
            return await _context.FoodImage.Where(x => x.SearchAPI == searchAPI).Where(x => x.FoodName == foodName).CountAsync();
        }

        [HttpPost]
        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            var hit = await FindAsync(foodImage.BaseUrl);
            if (hit != null) return Conflict();

            _context.FoodImage.Add(foodImage);
            await _context.SaveChangesAsync();
            return foodImage;
        }

        private async Task<FoodImage?> FindAsync(string baseUrl)
            => await _context.FoodImage.SingleOrDefaultAsync(x => x.BaseUrl == baseUrl);
    }
}
