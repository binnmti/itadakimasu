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
        private const string BlobUrl = "https://itadakimasu.blob.core.windows.net";
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
            int fileCount = 1;
            var sb = new StringBuilder();
            foreach(var imageUrl in blobAdapter.GetBlobUrls("foodimage"))
            {
                sb.Append(imageUrl);
                try
                {
                    //TODO:フォルダが変わってもナンバリングが続く。

                    var stream = await HttpClient.GetStreamAsync(imageUrl);
                    var foodFolder = Path.GetFileName(Path.GetDirectoryName(imageUrl));
                    var filePath = Path.Combine(PathTextBox.Text, foodFolder);
                    Directory.CreateDirectory(filePath);
                    new Bitmap(stream).Save(Path.Combine(filePath, $"{fileCount:0000}.jpg"));
                    fileCount++;
                    sb.Append($":{fileCount:0000}.jpg");
                }
                catch (Exception ex)    
                {
                    sb.Append($":{ex.Message}");
                }
                sb.AppendLine("");
            }
            await File.AppendAllTextAsync(Path.Combine(PathTextBox.Text, "log.txt"), sb.ToString());

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