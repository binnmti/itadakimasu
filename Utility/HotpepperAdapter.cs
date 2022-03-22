namespace Utility
{
    public class HotpepperAdapter
    {
        private static readonly string HotpepperUrl = "http://webservice.recruit.co.jp/hotpepper/gourmet/v1/?";
        private string HotpepperKey { get; }
        private HttpClient HttpClient { get; }

        public HotpepperAdapter(string key, HttpClient httpClient)
        {
            HotpepperKey = key;
            HttpClient = httpClient;
        }

        public async Task<string> GetResultAsync(string lat, string lng)
        {
            return await HttpClient.GetStringAsync($"{HotpepperUrl}key={HotpepperKey}&lat={lat}&lng={lng}");
        }
    }
}
