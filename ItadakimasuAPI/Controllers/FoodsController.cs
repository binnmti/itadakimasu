using Microsoft.AspNetCore.Mvc;
using ItadakimasuAPI.Models;
using Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

namespace ItadakimasuAPI.Controllers
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

            using var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
            var bingCustomSearchSubscriptionKey = await GetSecretValueAsync(client, "BingCustomSearchSubscriptionKey");
            var bingCustomSearchCustomConfigId = await GetSecretValueAsync(client, "BingCustomSearchCustomConfigId");
            var customVisionTrainingKey = await GetSecretValueAsync(client, "CustomVisionTrainingKey");
            var customVisionProjectId = await GetSecretValueAsync(client, "CustomVisionProjectId");

            //認識できない画像の場合は不正チェックにもなるので時間がかかっても最初にやる
            var urlList = BingSearchUtility.GetContentUrlList(HttpClient, name, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
            CustomVisionUtility.Upload(customVisionTrainingKey, customVisionProjectId, name, urlList);
            var food = new Food() { Name = name };
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Food), new { id = food.Id }, food);
        }
    }
}
