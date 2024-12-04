using MauiStartFrom.Resources.Strings;
using System.Globalization;

namespace MauiStartFrom;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        string lang = Preferences.Get("LanguageSaved", "en-US");
        Thread.CurrentThread.CurrentCulture =
        Thread.CurrentThread.CurrentUICulture =
        CultureInfo.CurrentCulture =
        CultureInfo.CurrentUICulture =
        AppStrings.Culture = new CultureInfo(lang);
        return new Window(new AppShell());
    }
}