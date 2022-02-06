using System.Text.Json;

namespace Utility
{
    public static class BingSearchUtility
    {
        private readonly static string BingUrl = "https://api.bing.microsoft.com";
        public class BingCustomSearchResponse
        {
            public string _type { get; set; } = "";
            public int totalEstimatedMatches { get; set; }
            public int nextOffset { get; set; }

            public WebPage[] value { get; set; } = null!;
        }

        public class WebPage
        {
            public string name { get; set; } = "";
            public string webSearchUrl { get; set; } = "";
            public string hostPageUrl { get; set; } = "";
            public OpenGraphImage thumbnail { get; set; } = null!;
            public string contentUrl { get; set; } = "";
        }

        public class OpenGraphImage
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public static async Task<List<string>> GetContentUrlListAsync(HttpClient client, string searchTerm, string subscriptionKey, string customConfigId)
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            var contentUrlList = new List<string>();
            int nextOffset = 0;
            int count = 0;
            do
            {
                var url = $"{BingUrl}/v7.0/custom/images/search?q={searchTerm}&customconfig={customConfigId}&mkt=ja-JP&offset={nextOffset}";
                HttpResponseMessage httpResponseMessage = default;
                try
                {
                    httpResponseMessage = await client.GetAsync(url);
                }
                catch (Exception ex)
                {
                    client = new HttpClient();
                    Console.WriteLine($"GetContentUrlListAsync失敗:{ex.Message}:{ex.StackTrace}");
                    continue;
                }

                var responseContent = await httpResponseMessage.Content.ReadAsStreamAsync();
                var response = await JsonSerializer.DeserializeAsync<BingCustomSearchResponse>(responseContent) ?? new BingCustomSearchResponse();
                count = response.totalEstimatedMatches;
                nextOffset = response.nextOffset;
                contentUrlList.AddRange(response.value.Select(x => x.contentUrl));

                Thread.Sleep(1000);
            } while (count > nextOffset);
            return contentUrlList.Distinct().ToList();
        }

    }
}
