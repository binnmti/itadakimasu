using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Itadakimasu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodImagesController : ControllerBase
    {
        private readonly ItadakimasuContext _context;
        private SignInManager<IdentityUser> SignInManager { get; }

        public FoodImagesController(ItadakimasuContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            SignInManager = signInManager;
        }

        public record Food(string Name, int FoodImageCount, FoodImage First);
        [HttpGet("food-list")]
        public async Task<List<Food>> FoodList(int stateNumber = 0)
            => await _context.FoodImage.StateNumber(stateNumber).GroupBy(x => x.FoodName).Select(x => new Food(x.Key, x.Count(), x.First())).ToListAsync();

        [HttpGet("food-name-food-image-list")]
        public Dictionary<string, List<FoodImage>> FoodNameFoodImageList(int count)
           => _context.FoodImage.ToLookup(x => x.FoodName).ToDictionary(x => x.Key, x => x.Select(s => s).Take(count).ToList());

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

        [Authorize]
        public record FoodImageRequest(long Id, int StateNumber);
        [HttpPost("food-image-state")]
        public async Task<ActionResult<FoodImage>> FoodImageState([FromBody] FoodImageRequest request)
        {
#if !DEBUG
            //TODO:これでもまだ弱い。本当は管理者のみ
            if (!SignInManager.IsSignedIn(User)) return Unauthorized();
#endif
            var hit = await _context.FoodImage.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (hit == null) return Conflict();

            hit.StatusNumber = request.StateNumber;
            await _context.SaveChangesAsync();
            return hit;
        }

        [Authorize]
        public record FoodImageAllRequest(List<long> Ids, int StateNumber);
        [HttpPost("food-image-all-state")]
        public void FoodImageAllState([FromBody] FoodImageAllRequest request)
        {
#if !DEBUG
            if (!SignInManager.IsSignedIn(User)) return;
#endif
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

//        [HttpPost("food-image-default-state")]
//        public async Task FoodImageDefaultState()
//        {
//#if !DEBUG
//            //TODO:これでもまだ弱い。本当は管理者のみ
//            if (!SignInManager.IsSignedIn(User)) return;
//#endif
//            foreach (var foodImages in _context.FoodImage.ToLookup(x => x.FoodName))
//            {
//                for (int i = 1; i <= 4; i++)
//                {
//                    foreach (var f in foodImages.Skip((i - 1) * 50).Take(50))
//                    {
//                        f.StatusNumber = i;
//                    }
//                }
//            }
//            await _context.SaveChangesAsync();
//        }

        private async Task<FoodImage?> FindAsync(string baseUrl)
            => await _context.FoodImage.SingleOrDefaultAsync(x => x.BaseUrl == baseUrl);
    }

    internal static class FoodImageExtention
    {
        internal static IQueryable<FoodImage> StateNumber(this IQueryable<FoodImage> foodImages, int stateNumber)
            => stateNumber == 0 ? foodImages : foodImages.Where(x => x.StatusNumber == stateNumber);
    }
}
