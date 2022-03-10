using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodNameGenerator;

class Program
{
#if DEBUG
    private static readonly string ItadakimasuApiUrl = "https://localhost:7162/api/";
#else
    private static readonly string ItadakimasuApiUrl = "https://itadakimasu.azurewebsites.net/api/";
#endif
    private static HttpClient HttpClient = new();

    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
        var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
        var builder = new ConfigurationBuilder().AddEnvironmentVariables();
        if (isDevelopment) builder.AddUserSecrets<Program>();

        var configuration = builder.Build();
        var customVisionProjectId = configuration["CustomVisionProjectId"];
        var customVisionTrainingKey = configuration["CustomVisionTrainingKey"];
        var customVisionpPredictionKey = configuration["CustomVisionpPredictionKey"];
        var userName = configuration["ItadakimasuUser"];
        var password = configuration["ItadakimasuPassword"];

        HttpClient.Timeout = TimeSpan.FromSeconds(5000);
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
        var accessToken = await HttpClient.GetStringAsync($"{ItadakimasuApiUrl}foodimages/get-access-token?userName={userName}&password={password}");
        HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        await CustomVision.Update(HttpClient, ItadakimasuApiUrl, customVisionTrainingKey, customVisionpPredictionKey, customVisionProjectId);
        //await BlobForBingSearchResult.Update(blobConnectionString, ItadakimasuApiUrl, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
    }
}
