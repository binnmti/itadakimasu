using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Utility;

namespace UtilityTestProject
{
    [TestClass]
    public class UnitTest1
    {
        IConfiguration Configuration { get; set; }

        public UnitTest1()
        {
            Configuration = new ConfigurationBuilder().AddUserSecrets<UnitTest1>().Build();
        }

        private static readonly HttpClient HttpClient = new();

        [TestMethod]
        public async Task TestMethod1()
        {
            var hotpepperApiKey = Configuration["HotpepperApiKey"];
            var (lat, lng) = ImageSharpAdapter.GetGps(@"C:\Users\BinMatsui\OneDrive\デスクトップ\GPSON.jpg");
            var hotpepper = new HotpepperAdapter(hotpepperApiKey, HttpClient);
            var result = await hotpepper.GetResultAsync(lat.ToString(), lng.ToString());
        }
    }
}