using Itadakimasu;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace FoodImageGenerator;

internal static class CustomVision
{
    private const int TrainingImagesMax = 150;

    internal static async Task Update(HttpClient httpClient, string itadakimasuApiUrl,  string customVisionTrainingKey, string customVisionpPredictionKey, string customVisionProjectId)
    {
        var customVisionWarpper = new CustomVisionWarpper(httpClient, customVisionTrainingKey, customVisionpPredictionKey, customVisionProjectId);
        //とりあえずcountを倍プッシュ。
        var foodNameFoodImages = await httpClient.GetFromJsonAsync<Dictionary<string, List<FoodImage>>>($"{itadakimasuApiUrl}foodimages/food-name-food-image-list?count={TrainingImagesMax * 2}");
        int co = 1;
        foreach (var foodNameFoodImage in foodNameFoodImages)
        {
            int skip = 0;
            int take = foodNameFoodImage.Value.Count(x => x.StatusNumber == 1) == TrainingImagesMax ? 0 : TrainingImagesMax;

            //全部がOKになるまで更新し続ける
            while (take != 0)
            {
                var foodImageList = foodNameFoodImage.Value.Skip(skip).Take(take).ToList();
                var resultList = customVisionWarpper.CreateTrainingImages(foodNameFoodImage.Key, foodImageList.Select(x => x.ToBlobUrl()).ToList());
                skip += take;
                take = resultList.Sum(x => x.Images.Count(x => x.Status != "OK"));
                //結果は１つずつpost。まとめたAPIを作っても良いかも。。
                var foodImageListCount = 0;
                foreach (var result in resultList)
                {
                    for (int i = 0; i < result.Images.Count; i++)
                    {
                        var foodImage = foodImageList[foodImageListCount];
                        foodImageListCount++;
                        if (foodImage.StatusNumber != 0) continue;
                        bool isOK = result.Images[i].Status == "OK";

                        var stateNumber = isOK ? 1 : -1;
                        var statusReason = isOK ? "" : result.Images[i].Status;
                        var testResult = "";
                        var json = JsonContent.Create(new { foodImage.Id, stateNumber, statusReason, testResult });
                        var r = await httpClient.PostAsync($"{itadakimasuApiUrl}foodimages/set-food-image-state-token", json);
                    }
                }
                Thread.Sleep(1000);
            }
            Console.WriteLine($"{foodNameFoodImage.Key}:{co++}/{foodNameFoodImages.Count}");
        }
    }

    internal static async Task Test(HttpClient httpClient, string itadakimasuApiUrl, string customVisionTrainingKey, string customVisionpPredictionKey, string customVisionProjectId)
    {
        var customVisionWarpper = new CustomVisionWarpper(httpClient, customVisionTrainingKey, customVisionpPredictionKey, customVisionProjectId);
        var foodNameFoodImages = await httpClient.GetFromJsonAsync<Dictionary<string, List<FoodImage>>>($"{itadakimasuApiUrl}foodimages/food-name-food-image-list?count=200");
        int co = 1;
        foreach (var foodNameFoodImage in foodNameFoodImages)
        {
            var foodImageList = foodNameFoodImage.Value.ToList();
            for (int i = 0; i < foodImageList.Count; i++)
            {
                var foodImage = foodImageList[i];
                var testResult = "";
                try
                {
                    testResult = customVisionWarpper.TestIteration(foodImage.FoodName, foodImage.ToBlobUrl());
                    if (testResult == "") continue;
                }
                catch (CustomVisionErrorException ex)
                {
                    testResult = ex.Message;
                    Console.WriteLine(ex.Message);
                }

                int stateNumber = 0;
                string statusReason = "";
                var json = JsonContent.Create(new { foodImage.Id, stateNumber, statusReason, testResult });
                var r = await httpClient.PostAsync($"{itadakimasuApiUrl}foodimages/set-food-image-state", json);
            }
            Thread.Sleep(1000);
            Console.WriteLine($"{foodNameFoodImage.Key}:{co++}/{foodNameFoodImages.Count}");
        }
    }
}
