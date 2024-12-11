namespace MauiStartFrom.Pages;

public partial class WeatherListPage : ContentPage
{
    public WeatherListPage(WeatherListPageModel model)
    {
        BindingContext = model;
        InitializeComponent();
    }
}