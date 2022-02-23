using Azure;
using Azure.Storage.Blobs.Models;
using AzureExploer.Properties;
using System.Text;
using System.Net.Http.Json;
using Utility;
using Models;
//

namespace AzureExploer
{
    public partial class Form1 : Form
    {
#if DEBUG
        ////private static readonly string ItadakimasuApiUrl = "https://localhost:7162/api/";
        private static readonly string ItadakimasuApiUrl = "https://itadakimasu.azurewebsites.net/api/";
#else
        private static readonly string ItadakimasuApiUrl = "https://itadakimasu.azurewebsites.net/api/";
#endif
        private const string BlobUrl = "https://itadakimasu.blob.core.windows.net";
        private static HttpClient Client { get; } = new();

        public Form1()
        {
            InitializeComponent();
        }

        private static string GetSaveDirectory(string pathName, string searchTerm)
        {
            var savePath = Path.Combine(pathName, searchTerm);
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            int dirNameCount = 1;
            while (true)
            {
                var uniquePath = Path.Combine(savePath, dirNameCount.ToString("0000"));
                if (!Directory.Exists(uniquePath))
                {
                    Directory.CreateDirectory(uniquePath);
                    return uniquePath;
                };
                dirNameCount++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            
            //var foods = await Client.GetFromJsonAsync<List<Food>>($"{ItadakimasuApiUrl}/foods/food-list") ?? new List<Food>();
            //var foodImages = await Client.GetFromJsonAsync<List<FoodImage>>($"{ItadakimasuApiUrl}/foodimages/food-image-list/{foods.First().Name}") ?? new List<FoodImage>();
            //treeView1.Nodes.AddRange(foods.Select(x => new TreeNode(x.Name)).ToArray());


            //foreach (var foodImage in foodImages.ToViewFoodImages().Select((val,idx) => (val,idx)))
            //{
            //    var stream = await Client.GetStreamAsync(foodImage.val.BlobUrl);
            //    imageList1.Images.Add(new Bitmap(stream));
            //    listView1.Items.Add(foodImage.val.Name, foodImage.idx);
            //}

            button1.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Key = BingKeyTextBox.Text;
            Settings.Default.Id = BingIdTextBox.Text;
            Settings.Default.Path = PathTextBox.Text;
            Settings.Default.SearchTerm = SearchTermTextBox.Text;
            Settings.Default.BlobKey = BlobKeyTextBox.Text;
            Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BlobKeyTextBox.Text = Settings.Default.BlobKey;
            BingKeyTextBox.Text = Settings.Default.Key;
            BingIdTextBox.Text = Settings.Default.Id;
            PathTextBox.Text = Settings.Default.Path;
            SearchTermTextBox.Text = Settings.Default.SearchTerm;
        }

        private void BlobKeyTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}