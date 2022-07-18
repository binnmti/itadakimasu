using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodImageGenerator;

class Program
{
#if DEBUG
    private static readonly string ItadakimasuApiUrl = "https://localhost:7162/api/";
#else
    private static readonly string ItadakimasuApiUrl = "https://itadakimasu.azurewebsites.net/api/";
#endif
    private static readonly HttpClient HttpClient = new();

    private enum FoodNameGeneratorIteration
    {
        BlobForBingSearchResult,
        CustomVisionUpdate,
        CustomVisionTest,
    }
    private static readonly FoodNameGeneratorIteration Iteration = FoodNameGeneratorIteration.CustomVisionTest;

    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
        var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
        var builder = new ConfigurationBuilder().AddEnvironmentVariables();
        if (isDevelopment) builder.AddUserSecrets<Program>();

        var configuration = builder.Build();
        var blobConnectionString = configuration["BlobConnectionString"];
        var bingCustomSearchSubscriptionKey = configuration["BingCustomSearchCustomConfigId"];
        var bingCustomSearchCustomConfigId = configuration["BingCustomSearchCustomConfigId"];
        var customVisionProjectId = configuration["CustomVisionProjectId"];
        var customVisionTrainingKey = configuration["CustomVisionTrainingKey"];
        var customVisionpPredictionKey = configuration["CustomVisionpPredictionKey"];
        var userName = configuration["ItadakimasuUser"];
        var password = configuration["ItadakimasuPassword"];

        HttpClient.Timeout = TimeSpan.FromSeconds(5000);
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
        var accessToken = await HttpClient.GetStringAsync($"{ItadakimasuApiUrl}foodimages/get-access-token?userName={userName}&password={password}");
        HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        switch (Iteration)
        {
            case FoodNameGeneratorIteration.BlobForBingSearchResult:
                await BlobForBingSearchResult.Update(HttpClient, blobConnectionString, ItadakimasuApiUrl, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
                break;
            case FoodNameGeneratorIteration.CustomVisionUpdate:
                await CustomVision.Update(HttpClient, ItadakimasuApiUrl, customVisionTrainingKey, customVisionpPredictionKey, customVisionProjectId);
                break;
            case FoodNameGeneratorIteration.CustomVisionTest:
                await CustomVision.Test(HttpClient, ItadakimasuApiUrl, customVisionTrainingKey, customVisionpPredictionKey, customVisionProjectId);
                break;
        };
    }
}
