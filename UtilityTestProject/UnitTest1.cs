using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var filePath = @"C:\Users\BinMatsui\OneDrive\デスクトップ\PXL_20220424_054909222.jpg";
            var data = File.ReadAllBytes(filePath);
            var content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //https://itadakimasu.azurewebsites.net/api/foods/get-food-image-result
            var result = await HttpClient.PostAsync("https://localhost:7162/api/foods/get-food-image-result", content);
            var str = await result.Content.ReadAsStringAsync();
        }
    }
}