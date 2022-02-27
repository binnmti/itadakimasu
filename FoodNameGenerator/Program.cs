using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    private static readonly string SearchAPI = "bing";
    private static readonly string BlobFoderName = "foodimage";
    private static readonly List<string> FoodNameList = new()
    {
        "オムライス",
        "餃子",
        "肉じゃが",
        "カレー",
        "牛丼",
        "親子丼",
        "豚の生姜焼き",
        "グラタン",
        "唐揚げ",
        "コロッケ",
        "シチュー",
        "天ぷら",
        "ローストビーフ",
        "豚の角煮",
        "チキン南蛮",
        "ピーマンの肉詰め",
        "ロールキャベツ",
        "スペアリブ",
        "ローストチキン",
        "もつ煮込み",
        "ミートボール",
        "ミートローフ",
        "牛すじ煮込み",
        "とんかつ",
        "ポークソテー",
        "つくね",
        "焼き豚",
        "煮豚",
        "ステーキ",
        "トンテキ",
        "肉巻き",
        "ローストポーク",
        "ぶり大根",
        "ぶりの照り焼き",
        "さばの味噌煮",
        "煮魚",
        "あさりの酒蒸し",
        "鮭のムニエル",
        "南蛮漬け",
        "焼き魚",
        "鮭のホイル焼き",
        "いわしのつみれ",
        "かつおのたたき",
        "いわしの梅煮",
        "かぶら蒸し",
        "ゆで卵",
        "温泉卵",
        "だし巻き卵",
        "茶碗蒸し",
        "キッシュ",
        "オムレツ",
        "かに玉",
        "スクランブルエッグ",
        "煮卵",
        "目玉焼き",
        "ニラ玉",
        "ポーチドエッグ",
        "スコッチエッグ",
        "卵とじ",
        "薄焼き卵",
        "炒り卵",
        "オムライス",
        "チャーハン",
        "パエリア",
        "タコライス",
        "チキンライス",
        "ハヤシライス",
        "ロコモコ",
        "ピラフ",
        "寿司",
        "おにぎり",
        "カルボナーラ",
        "ミートソース",
        "ナポリタン",
        "ペペロンチーノ",
        "ジェノベーゼ",
        "ペスカトーレ",
        "明太子パスタ",
        "ボンゴレ",
        "アラビアータ",
        "トマトクリームパスタ",
        "納豆パスタ",
        "きのこパスタ",
        "ツナパスタ",
        "ニョッキ",
        "ラザニア",
        "ラビオリ",
        "うどん",
        "蕎麦",
        "そうめん",
        "焼きそば",
        "ラーメン",
        "冷やし中華",
        "つけ麺",
        "お好み焼き",
        "たこ焼き",
        "味噌汁",
        "豚汁",
        "けんちん汁",
        "お吸い物",
        "かぼちゃスープ",
        "野菜スープ",
        "クラムチャウダー",
        "コーンスープ",
        "トマトスープ",
        "コンソメスープ",
        "クリームスープ",
        "中華スープ",
        "和風スープ",
        "韓国風スープ",
        "ポトフ",
        "すき焼き",
        "しゃぶしゃぶ",
        "おでん",
        "寄せ鍋",
        "キムチ鍋",
        "トマト鍋",
        "カレー鍋",
        "豆乳鍋",
        "もつ鍋",
        "石狩鍋",
        "水炊き",
        "湯豆腐",
        "きりたんぽ鍋",
        "雪見鍋",
        "火鍋",
        "キムチチゲ鍋",
        "ちゃんこ鍋",
        "牡蠣鍋",
        "カニ鍋",
        "ねぎま鍋",
        "鴨鍋",
        "あんこう鍋",
        "白味噌鍋",
        "ミルフィーユ鍋",
        "蒸し鍋",
        "ポテトサラダ",
        "タラモサラダ",
        "マカロニサラダ",
        "スパゲティサラダ",
        "シーザーサラダ",
        "大根サラダ",
        "春雨サラダ",
        "コールスロー",
        "キャロットラペ",
        "かぼちゃサラダ",
        "ごぼうサラダ",
        "コブサラダ",
        "ホットサラダ",
        "ジャーサラダ",
        "サンドイッチ",
        "フレンチトースト",
        "食パン",
        "蒸しパン",
        "ホットサンド",
        "惣菜パン",
        "菓子パン",
        "クロワッサン",
        "ハードブレッド",
        "天然酵母パン",
        "クッキー",
        "スイートポテト",
        "チーズケーキ",
        "シフォンケーキ",
        "パウンドケーキ",
        "ケーキ",
        "ホットケーキ",
        "チョコレート",
        "スコーン",
        "マフィン",
        "プリン",
        "ドーナツ",
        "シュークリーム",
        "エクレア",
        "ゼリー",
        "ムース",
        "アイス",
        "シャーベット",
        "酢豚",
        "チンジャオロース",
        "八宝菜",
        "麻婆豆腐",
        "エビチリ",
        "エビマヨ",
        "回鍋肉",
        "バンバンジー",
        "油淋鶏",
        "よだれ鶏",
        "ビーフン",
        "ジャージャー麺",
        "坦々麺",
        "春巻き",
        "肉まん",
        "焼売",
        "杏仁豆腐",
        "中華ちまき",
        "酸辣湯",
        "チャプチェ",
        "チヂミ",
        "ビビンバ",
        "ナムル",
        "キムチ",
        "プルコギ",
        "スンドゥブ",
        "チョレギサラダ",
        "冷麺",
        "サムゲタン",
        "サムギョプサル",
        "クッパ",
        "タッカルビ",
        "カムジャタン",
        "トッポギ",
        "ケジャン",
        "テンジャンチゲ",
        "ピザ",
        "ミネストローネ",
        "リゾット",
        "バーニャカウダ",
        "カルパッチョ",
        "アクアパッツァ",
        "ピカタ",
        "ブルスケッタ",
        "パニーニ",
        "カルツォーネ",
        "カプレーゼ",
        "パンナコッタ",
        "ティラミス",
        "ラタトゥイユ",
        "チーズフォンデュ",
        "テリーヌ",
        "ブイヤベース",
        "ムニエル",
        "ビスク",
        "マリネ",
        "ガレット",
        "ゴーヤチャンプル",
        "沖縄そば",
        "海ぶどう",
        "そうめんチャンプルー",
        "ラフテー",
        "ミミガー",
        "ジューシー",
        "サーターアンダーギー",
        "ヒラヤーチー",
        "島唐辛子",
    };
    private static HttpClient HttpClient = new();

    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
        var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
        var builder = new ConfigurationBuilder().AddEnvironmentVariables();
        if (isDevelopment) builder.AddUserSecrets<Program>();

        var configuration = builder.Build();
        var blobConnectionString = configuration["BlobConnectionString"];
        var bingCustomSearchSubscriptionKey = configuration["BingCustomSearchSubscriptionKey"];
        var bingCustomSearchCustomConfigId = configuration["BingCustomSearchCustomConfigId"];
        var customVisionTrainingKey = configuration["CustomVisionTrainingKey"];
        var customVisionProjectId = configuration["CustomVisionProjectId"];
        HttpClient.Timeout = TimeSpan.FromSeconds(5000);
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
        var customVisionWarpper = new CustomVisionWarpper(HttpClient, customVisionTrainingKey, customVisionProjectId);
        var foodNameFoodImages = await HttpClient.GetFromJsonAsync<Dictionary<string, List<FoodImage>>>($"{ItadakimasuApiUrl}foodimages/food-name-food-image-list?count=100");

        int co = 1;
        foreach (var foodNameFoodImage in foodNameFoodImages)
        {
            int skip = 0;
            int take = 50;
            while (take != 0)
            {
                var foodImageList = foodNameFoodImage.Value.Skip(skip).Take(take).ToList();
                var result = customVisionWarpper.CreateTrainingImages(foodNameFoodImage.Key, foodImageList.Select(x => x.ToBlobUrl()).ToList());
                skip += take;
                take = result.Images.Count(x => !x.Status.Contains("OK"));
                for (int i = 0; i < foodImageList.Count; i++)
                {
                    var foodImage = foodImageList[i];
                    var StateNumber = result.Images[i].Status.Contains("OK") ? 1 : -1;
                    var json = JsonSerializer.Serialize(new { foodImage.Id, StateNumber });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var r = await HttpClient.PostAsync($"{ItadakimasuApiUrl}foodimages/food-image-state", content);
                }
            }
            Console.WriteLine($"{foodNameFoodImage.Key}:{co++}/{foodNameFoodImages.Count}");
        }

        //await UpdateBlobForBing(blobConnectionString, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
    }

    private static async Task UpdateBlobForBing(string blobConnectionString, string bingCustomSearchSubscriptionKey, string bingCustomSearchCustomConfigId)
    {
        var blobAdapter = new BlobAdapter(blobConnectionString);
        foreach (var food in FoodNameList.Select((val, idx) => (val, idx)))
        {
            Console.WriteLine($"{food.val}:開始");
            //TODO:Foodが既にあっても、FoodImagesは更新したい。
            //var foodResult = await HttpClient.GetAsync($"{ItadakimasuApiUrl}Foods/Food?name={food.val}");
            //if (foodResult.IsSuccessStatusCode) continue;

            var urlList = await BingSearchUtility.GetContentUrlListAsync(HttpClient, food.val, bingCustomSearchSubscriptionKey, bingCustomSearchCustomConfigId);
            foreach (var url in urlList.Select((val, idx) => (val, idx)))
            {
                Console.WriteLine($"{food.val}:{food.idx + 1}/{FoodNameList.Count}:{url.idx + 1}/{urlList.Count}:{url.val}");

                var newName = await HttpClient.GetFromJsonAsync<int>($"{ItadakimasuApiUrl}FoodImages/get-new-name?baseUrl={HttpUtility.UrlEncode(url.val)}&searchAPI={SearchAPI}&foodName={food.val}");
                if (newName == -1)
                {
                    //TODO:重複でも更新したいケースはさてどうするか。
                    Console.WriteLine($"DB重複");
                    continue;
                }
                Stream stream = default;
                try
                {
                    stream = await HttpClient.GetStreamAsync(url.val);
                }
                catch (Exception ex)
                {
                    //作り直し
                    HttpClient = new HttpClient();
                    Console.WriteLine($"GetStreamAsync失敗:{ex.Message}:{ex.StackTrace}");
                    continue;
                }
                ImageSharpAdapter.Jpeg jpeg = default;
                try
                {
                    jpeg = ImageSharpAdapter.ConvertJpeg(stream, 300, 300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ImageSharpAdapter変換:{ex.Message}:{ex.StackTrace}");
                    continue;
                }

                var fileName = $"{SearchAPI}{newName:0000}";
                try
                {
                    blobAdapter.Upload(jpeg.Image, BlobFoderName, $"{food.val}/{fileName}.jpg");
                    blobAdapter.Upload(jpeg.ThumbnailImage, BlobFoderName, $"{food.val}/{fileName}_s.jpg");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Upload失敗:{ex.Message}:{ex.StackTrace}");
                    continue;
                }

                var foodImage = new FoodImage()
                {
                    FoodName = food.val,
                    BaseUrl = url.val,
                    SearchAPI = SearchAPI,
                    BlobName = $"{fileName}.jpg",
                    BlobWidth = jpeg.Width,
                    BlobHeight = jpeg.Height,
                    BlobSize = jpeg.Image.Length,
                    BlobSName = $"{fileName}_s.jpg",
                    BlobSWidth = jpeg.ThumbnailWidth,
                    BlobSHeight = jpeg.ThumbnailHeight,
                    BlobSSize = jpeg.ThumbnailImage.Length,
                };
                var result = await HttpClient.PostAsJsonAsync($"{ItadakimasuApiUrl}FoodImages", foodImage);
                Console.WriteLine(result.IsSuccessStatusCode ? $"DB成功" : $"DB失敗:{result.ReasonPhrase}");
                Thread.Sleep(1000);
            };
            await HttpClient.PostAsync($"{ItadakimasuApiUrl}Foods?name={food.val}", null);
            Console.WriteLine($"{food.val}:終了:{food.idx + 1}/{FoodNameList.Count}");
        }
    }
}
