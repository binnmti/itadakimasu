using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility;

namespace FoodNameGenerator
{
    internal static class CustomVision
    {
        private const int TrainingImagesMax = 50;

        internal static async Task Update(HttpClient httpClient, string itadakimasuApiUrl,  string customVisionTrainingKey, string customVisionProjectId)
        {
            var customVisionWarpper = new CustomVisionWarpper(httpClient, customVisionTrainingKey, customVisionProjectId);
            //とりあえず倍プッシュ。
            var foodNameFoodImages = await httpClient.GetFromJsonAsync<Dictionary<string, List<FoodImage>>>($"{itadakimasuApiUrl}foodimages/food-name-food-image-list?count={TrainingImagesMax * 2}");

            //TODO:認証
            int co = 1;
            foreach (var foodNameFoodImage in foodNameFoodImages)
            {
                int skip = 0;
                int take = TrainingImagesMax;
                //全部がOKになるまで更新し続ける
                while (take != 0)
                {
                    var foodImageList = foodNameFoodImage.Value.Skip(skip).Take(take).ToList();
                    var result = customVisionWarpper.CreateTrainingImages(foodNameFoodImage.Key, foodImageList.Select(x => x.ToBlobUrl()).ToList());
                    skip += take;
                    take = result.Images.Count(x => !x.Status.Contains("OK"));
                    //結果は１つずつpost。まとめたAPIを作っても良いかも。。
                    for (int i = 0; i < foodImageList.Count; i++)
                    {
                        var foodImage = foodImageList[i];
                        var StateNumber = result.Images[i].Status.Contains("OK") ? 1 : -1;

                        var json = JsonContent.Create(new { foodImage.Id, StateNumber });
                        var r = await httpClient.PostAsync($"{itadakimasuApiUrl}foodimages/food-image-state", json);
                    }
                }
                Console.WriteLine($"{foodNameFoodImage.Key}:{co++}/{foodNameFoodImages.Count}");
            }
        }
    }
}
