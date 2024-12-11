using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiStartFrom.PageModels;

public partial class WeatherListPageModel : ObservableObject
{
    private readonly XxxWebService _webService;

    [ObservableProperty]
    private List<Forecast> _forecasts = [];

    public WeatherListPageModel(XxxWebService webService)
    {
        _webService = webService;
    }

    [RelayCommand]
    private async Task Appearing()
    {
        Forecasts = await LoadForecasts();
    }

    private async Task<List<Forecast>> LoadForecasts()
    {
        List<Forecast>? retList = null;
        if (await _webService.Login("user@xyz.com", "password") != null)
            retList = await _webService.Weather();
        if (retList == null)
        {
            retList = new List<Forecast>();
            retList.Add(new Forecast(DateOnly.FromDateTime(DateTime.Now), 0, "Error Accessing the Web Service"));
        }
        return retList;
    }
}