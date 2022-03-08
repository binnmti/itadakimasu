using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Utility;

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
        var customVisionTrainingKey = configuration["CustomVisionTrainingKey"];
        var customVisionProjectId = configuration["CustomVisionProjectId"];
        var userName = configuration["ItadakimasuUser"];
        var password = configuration["ItadakimasuPassword"];

        HttpClient.Timeout = TimeSpan.FromSeconds(5000);
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
        //HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        //var json = JsonContent.Create(new { userName, password });
        //var r = await HttpClient.PostAsync($"{ItadakimasuApiUrl}foodimages/login", json);

        //await BlobForBingSearchResult.Update(blobConnectionString, ItadakimasuApiUrl, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
        await CustomVision.Update(HttpClient, ItadakimasuApiUrl, customVisionTrainingKey, customVisionProjectId);
    }
}
