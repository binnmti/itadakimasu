using ItadakimasuMaui.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ItadakimasuMaui.ViewModels;

public class MyFoodImageViewModel : BaseViewModel
{
    public HttpClient HttpClient { get; } = new HttpClient();

    public MyFoodImageViewModel()
    {
        Title = "MyFoodImage";
    }

    public async Task SetStreamAsync(Stream stream)
    {
        if (stream == null) return;

        FoodImage = ImageSource.FromStream(() => stream);
        HttpResponseMessage message;
        using (var ms = new MemoryStream())
        {
            stream.CopyTo(ms);
            stream.Seek(0, SeekOrigin.Begin);
            var content = new ByteArrayContent(ms.ToArray());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            message = await HttpClient.PostAsync("https://itadakimasu.azurewebsites.net/api/foods/get-food-image-result", content);
        }
        if (!message.IsSuccessStatusCode) return;

        var json = await message.Content.ReadAsStringAsync();
        var foodImageResult = JsonSerializer.Deserialize<FoodImageResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        FoodName = foodImageResult.FoodName;
        Lat = foodImageResult.Lat;
        Lng = foodImageResult.Lng;
        Shops = new ObservableCollection<Shop>(foodImageResult.Shops ?? new List<Shop>());
    }

    private ImageSource foodImage;
    public ImageSource FoodImage
    {
        get => foodImage;
        set => SetProperty(ref foodImage, value);
    }

    private string foodName;
    public string FoodName
    {
        get => foodName;
        set => SetProperty(ref foodName, value);
    }

    private string eatingPlace;
    public string EatingPlace
    {
        get => eatingPlace;
        set => SetProperty(ref eatingPlace, value);
    }

    private double lat;
    public double Lat
    {
        get => lat;
        set => SetProperty(ref lat, value);
    }

    private double lng;
    public double Lng
    {
        get => lng;
        set => SetProperty(ref lng, value);
    }

    private ObservableCollection<Shop> shops;
    public ObservableCollection<Shop> Shops
    {
        get => shops;
        set => SetProperty(ref shops, value);
    }

    private string memo;
    public string Memo
    {
        get => memo;
        set => SetProperty(ref memo, value);
    }
}
