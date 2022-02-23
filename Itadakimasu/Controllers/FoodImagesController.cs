using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Itadakimasu.Controllers
{
    internal static class FoodImageExtention
    {
        internal static IQueryable<FoodImage> StateNumber(this IQueryable<FoodImage> foodImages, int stateNumber)
            => stateNumber == 0 ? foodImages : foodImages.Where(x => x.StatusNumber == stateNumber);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FoodImagesController : ControllerBase
    {
        private readonly ItadakimasuContext _context;

        public FoodImagesController(ItadakimasuContext context)
        {
            _context = context;
        }

        public record Food(string Name, int FoodImageCount, FoodImage First);
        [HttpGet("food-list")]
        public async Task<List<Food>> FoodList(int stateNumber = 0)
            => await _context.FoodImage.StateNumber(stateNumber).GroupBy(x => x.FoodName).Select(x => new Food(x.Key, x.Count(), x.First())).ToListAsync();

        [HttpGet("food-image-count")]
        public int FoodImageCount() => _context.FoodImage.Count();

        [HttpGet("food-image-list/{foodName}")]
        public async Task<ActionResult<List<FoodImage>>> FoodImageList(string foodName, int page = 1, int count = 50, int stateNumber = 0)
        {
            page = Math.Max(page, 1);
            return await _context.FoodImage.StateNumber(stateNumber).Where(x => x.FoodName == foodName).Skip((page - 1) * count).Take(count).ToListAsync();
        }

        [HttpGet("food-image-list-count/{foodName}")]
        public async Task<ActionResult<int>> FoodImageListCount(string foodName, int stateNumber = 0)
            => await _context.FoodImage.StateNumber(stateNumber).CountAsync(x => x.FoodName == foodName);


        public record FoodImageRequest(long Id, int StateNumber);
        [HttpPost("food-image-state")]
        public async Task<ActionResult<FoodImage>> FoodImageState([FromBody] FoodImageRequest request)
        {
            var hit = await _context.FoodImage.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (hit == null) return Conflict();

            hit.StatusNumber = request.StateNumber;
            await _context.SaveChangesAsync();
            return hit;
        }

        public record FoodImageAllRequest(List<long> Ids, int StateNumber);
        [HttpPost("food-image-all-state")]
        public void FoodImageAllState([FromBody] FoodImageAllRequest request)
        {
            var hits = _context.FoodImage.Where(x => request.Ids.Any(i => i == x.Id)).ToList();
            hits.ForEach(x => x.StatusNumber = request.StateNumber);
            _context.SaveChanges();
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
