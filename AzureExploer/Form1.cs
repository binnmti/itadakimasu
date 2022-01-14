using AzureExploer.Properties;
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

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            var savePath = Path.Combine(PathTextBox.Text, SearchTermTextBox.Text);
            if(!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            int co = 1;
            while(true)
            {
                var newDir = Path.Combine(savePath, co.ToString());
                if (!Directory.Exists(newDir))
                {
                    Directory.CreateDirectory(newDir);
                    break;
                };
                co++;
            }
            var imageList = await BingSearchUtility.GetContentUrlListAsync(new HttpClient(), SearchTermTextBox.Text, keyTextBox.Text, IdTextBox.Text);
            foreach(var image in imageList)
            {
                try
                {
                    var res = await HttpClient.GetAsync(image, HttpCompletionOption.ResponseContentRead);
                    var match = Regex.Match(image, @".+/(.+?)([\?#;].*)?$");
                    using var fileStream = File.Create(Path.Combine(savePath, co.ToString(), match.Groups[1].Value));
                    using var httpStream = await res.Content.ReadAsStreamAsync();
                    httpStream.CopyTo(fileStream);
                }
                catch (Exception ex)    
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

            button1.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Key = keyTextBox.Text;
            Settings.Default.Id = IdTextBox.Text;
            Settings.Default.Path = PathTextBox.Text;
            Settings.Default.SearchTerm = SearchTermTextBox.Text;
            Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            keyTextBox.Text = Settings.Default.Key;
            IdTextBox.Text = Settings.Default.Id;
            PathTextBox.Text = Settings.Default.Path;
            SearchTermTextBox.Text = Settings.Default.SearchTerm;
        }
    }
}