using Microsoft.AspNetCore.Mvc;
using Itadakimasu.Models;
using Models;
using Microsoft.Azure.KeyVault;
using Itadakimasu.Data;

namespace Itadakimasu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly ItadakimasuContext _context;
        private readonly IConfiguration _configuration;
        private static readonly HttpClient HttpClient = new();

        public FoodsController(ItadakimasuContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private static async Task<string> GetSecretValueAsync(KeyVaultClient client, string key)
            => (await client.GetSecretAsync("https://itadakimasu.vault.azure.net/", key)).Value;

        [HttpPost]
        public async Task<ActionResult<Food>> Food(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest();

            var bingCustomSearchSubscriptionKey = _configuration.GetConnectionString("BingCustomSearchSubscriptionKey");
            var bingCustomSearchCustomConfigId = _configuration.GetConnectionString("BingCustomSearchCustomConfigId");
            var customVisionTrainingKey = _configuration.GetConnectionString("CustomVisionTrainingKey");
            var customVisionProjectId = _configuration.GetConnectionString("CustomVisionProjectId");

            //TODO:例えばaaaで検索しても何かヒットしてしまう。画像として料理じゃないかを取れれば大分エラーチェックにはなるが、現状その料理かどうかは判断が非常に難しい。。。。
            var urlList = BingSearchUtility.GetContentUrlList(HttpClient, name, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
            CustomVisionUtility.Upload(customVisionTrainingKey, customVisionProjectId, name, urlList);
            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Food), new { id = food.Id }, food);
        }
    }
}
