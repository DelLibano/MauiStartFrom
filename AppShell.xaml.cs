using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MauiStartFrom.Resources.Strings;
using Font = Microsoft.Maui.Font;

namespace MauiStartFrom;

public partial class AppShell : Shell
{
    private bool shouldRunSelectionChanged = true;

    public int AppThemeIndexSaved
    {
        get
        {
            var defaultIndex = Application.Current!.UserAppTheme == AppTheme.Light ? 0 : 1;
            return Preferences.Get(nameof(AppThemeIndexSaved), defaultIndex);
        }
        set
        {
            if (AppThemeIndexSaved != value)
            {
                Preferences.Set(nameof(AppThemeIndexSaved), value);
                OnPropertyChanged();
            }
        }
    }

    public string LanguageSaved
    {
        get
        {
            return Preferences.Get(nameof(LanguageSaved), AppStrings.DefaultLang);
        }
        set
        {
            Preferences.Set(nameof(LanguageSaved), value);
            OnPropertyChanged();
        }
    }

    public AppShell()
    {
        InitializeComponent();
        ThemeSegmentedControl.SelectedIndex = AppThemeIndexSaved;
        LanguageControl.SelectedIndex = LanguageSaved == AppStrings.DefaultLang ? 0 : 1;
    }

    public static async Task DisplaySnackbarAsync(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Color.FromArgb("#FF3300"),
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(0),
            Font = Font.SystemFontOfSize(18),
            ActionButtonFont = Font.SystemFontOfSize(14)
        };
        
        var snackbar = Snackbar.Make(message, visualOptions: snackbarOptions);
        await snackbar.Show(cancellationTokenSource.Token);
    }

    public static async Task DisplayToastAsync(string message)
    {
        // Toast is currently not working in MCT on Windows
        if (OperatingSystem.IsWindows())
            return;

        var toast = Toast.Make(message, textSize: 18);
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await toast.Show(cts.Token);
    }

    private void ThemeSelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        AppThemeIndexSaved = e.NewIndex == 0 ? 0 : 1;
        Application.Current!.UserAppTheme = e.NewIndex == 0 ? AppTheme.Light : AppTheme.Dark;
    }

    private async void LanguageSelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        if (!shouldRunSelectionChanged) return;
        if (e.OldIndex >= 0 && Application.Current is not null) //On Actual Change by user
        {
            if (await DisplayAlert(AppStrings.AreYouSure, AppStrings.ChangeLanguage, AppStrings.Yes, AppStrings.No))
            {
                LanguageSaved = e.NewIndex == 0 ? AppStrings.DefaultLang : AppStrings.OtherLang;
                Application.Current.Quit();
            }
            else
            {
                shouldRunSelectionChanged = false;
                LanguageControl.SelectedIndex = e.OldIndex;
                shouldRunSelectionChanged = true;
            }
        }
        else LanguageSaved = e.NewIndex == 0 ? AppStrings.DefaultLang : AppStrings.OtherLang; //On Initialization

    }
}
