using ItadakimasuMaui.Services;
using ItadakimasuMaui.ViewModels;

namespace ItadakimasuMaui.Views;

public partial class MyFoodImagePage : ContentPage
{
    MyFoodImageViewModel _viewModel;

    public MyFoodImagePage()
    {
        InitializeComponent();

        BindingContext = _viewModel = new MyFoodImageViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void OnSelectPhotoButtonClicked(object sender, EventArgs e)
    {
        (sender as Button).IsEnabled = false;

        var photo = new PhotoPickerService();
        var stream = await photo.GetImageStreamAsync();
        await _viewModel.SetStreamAsync(stream);

        (sender as Button).IsEnabled = true;
    }

    private async void OnTakePhotoButtonClicked(object sender, EventArgs e)
    {
        (sender as Button).IsEnabled = false;

        FileResult photo = default;
        try
        {
            photo = await MediaPicker.CapturePhotoAsync();
            if (photo == null) return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        var stream = await photo.OpenReadAsync();
        //TODO:Exif�ŏ�ɏc�ɂȂ�悤�ɂ�����

        //TODO:���̏ꍇ��Exif�Ɉʒu��񂪓����ĂȂ��B�����ɂ͂ǂ�����΁H
        //TODO:������Geo����ʒu���Ƃ�̂ł͐��x���Ⴄ�H

        await _viewModel.SetStreamAsync(stream);
        File.Delete(photo.FullPath);

        (sender as Button).IsEnabled = true;
    }
}