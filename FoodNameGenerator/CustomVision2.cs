using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace FoodNameGenerator
{
    internal static class CustomVision2
    {
        internal static async Task Update(HttpClient httpClient, string itadakimasuApiUrl,  string customVisionTrainingKey, string customVisionpPredictionKey, string customVisionProjectId)
        {
            var customVisionWarpper = new CustomVisionWarpper(httpClient, customVisionTrainingKey, customVisionpPredictionKey, customVisionProjectId);
            var foodNameFoodImages = await httpClient.GetFromJsonAsync<Dictionary<string, List<FoodImage>>>($"{itadakimasuApiUrl}foodimages/food-name-food-image-list?count=200");
            int co = 1;
            foreach (var foodNameFoodImage in foodNameFoodImages)
            {
                var foodImageList = foodNameFoodImage.Value.ToList();
                //結果は１つずつpost。まとめたAPIを作っても良いかも。。
                for (int i = 0; i < foodImageList.Count; i++)
                {
                    var foodImage = foodImageList[i];

                    var testResult = customVisionWarpper.TestIteration(foodImage.ToBlobUrl());

                    var json = JsonContent.Create(new { foodImage.Id, testResult });
                    var r = await httpClient.PostAsync($"{itadakimasuApiUrl}foodimages/food-image-test-result-jwt", json);
                }
                Thread.Sleep(1000);
                Console.WriteLine($"{foodNameFoodImage.Key}:{co++}/{foodNameFoodImages.Count}");
            }
        }
    }
}
