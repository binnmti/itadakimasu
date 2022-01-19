using Azure;
using Azure.Storage.Blobs.Models;
using AzureExploer.Properties;
using System.Text;
using System.Text.RegularExpressions;
using Utility;

namespace AzureExploer
{
    public partial class Form1 : Form
    {
        private static HttpClient HttpClient { get; } = new();

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

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            var blobAdapter = new BlobAdapter(BlobKeyTextBox.Text);
            var blobItems = blobAdapter.GetBlobItems("foodimage");

            foreach (var blobItem in blobItems)
            {
                Console.WriteLine("Blob name: {0}", blobItem.Name);
            }

            var imageUrlList = await BingSearchUtility.GetContentUrlListAsync(new HttpClient(), SearchTermTextBox.Text, BingKeyTextBox.Text, BingIdTextBox.Text);
            var savePath = GetSaveDirectory(PathTextBox.Text, SearchTermTextBox.Text);
            int fileCount = 1;

            var sb = new StringBuilder();
            foreach(var imageUrl in imageUrlList)
            {
                sb.Append(imageUrl);
                try
                {
                    var stream = await HttpClient.GetStreamAsync(imageUrl);
                    new Bitmap(stream).Save(Path.Combine(savePath, $"{fileCount:0000}.jpg"));
                    fileCount++;
                    sb.Append($":{fileCount:0000}.jpg");
                }
                catch (Exception ex)    
                {
                    sb.Append($":{ex.Message}");
                }
                sb.AppendLine("");
            }
            File.AppendAllTextAsync(Path.Combine(savePath, "log.txt"), sb.ToString());
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