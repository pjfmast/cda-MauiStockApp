using MauiStockApp.Views;
using MauiStockApp.Helpers;
using MauiStockTake.Resources.Themes;
using System.Reflection;
using MauiStockApp.Services;

namespace MauiStockApp;

public partial class AppShell : Shell
{
    private readonly IAuthService _authService;
	public AppShell(IAuthService authService)
	{
		InitializeComponent();
        _authService = authService;
 
        ThemeMenuItem.Text = "Switch to Sandy Theme";
        
        // Most page routes are registered by the Tab or Flyout item
        // The loginPage here is registered programmatically:
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

        // see 7.5.5, not used anymore
        //Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));

        // added for map demo
        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));


    }

    ShellContent _previousShellContent;

    // workaround for issue Shell, Navigation and Page instantiation
    // https://github.com/dotnet/maui/issues/9300#issuecomment-1416893792
    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);
        if (CurrentItem?.CurrentItem?.CurrentItem is not null &&
            _previousShellContent is not null)
        {
            var property = typeof(ShellContent)
                .GetProperty("ContentCache", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            property.SetValue(_previousShellContent, null);
        }
        LogoutMenuItem.Text = _authService.IsAuthenticated ? "Logout" : "Login";

        _previousShellContent = CurrentItem?.CurrentItem?.CurrentItem;
    }

    private void ThemeMenuItem_Clicked(object sender, EventArgs e)
    {
        if (App.Theme == Theme.Default)
        {
            App.Theme = Theme.Sandy;
            ThemeMenuItem.Text = "Switch to Default Theme";
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new SandyTheme());
            }
        }
        else
        {
            App.Theme = Theme.Default;
            ThemeMenuItem.Text = "Switch to Sandy Theme";
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new DefaultTheme());
            }
        }

        MessagingCenter.Send<AppShell>(this, "ThemeChanged");
    }

    private async void OnLogoutItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
        LogoutMenuItem.Text = _authService.IsAuthenticated ? "Logout" : "Login";
    }

    private async void OnMapItem_Clicked(object sender, EventArgs e)
    {
#if WINDOWS
        await Launcher.OpenAsync("bingmaps:?where=Avans HA");
#else
    await Shell.Current.GoToAsync(nameof(MapPage));
#endif
    }
}
