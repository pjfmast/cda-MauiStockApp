using MauiStockApp.Auth0;
using MauiStockApp.Services;

namespace MauiStockApp.Views;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _authService;

    public LoginPage(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;

        // To circumvent a known issue on WinUI
#if WINDOWS
    _authService.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif

        LoginButton.Text = Preferences.Get("IsLoggedIn", false) ? "Logout" : "Login";

    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (Preferences.Get("IsLoggedIn", false))
        {
            await LogoutAsync();
        } else
        {
            await LoginAsync();
        }
    }

    private async Task LoginAsync()
    {
        LoginButton.IsEnabled = false;
        LoggingIn.IsVisible = true;

        var loggingInResult = await _authService.LoginAsync();

        LoggingIn.IsVisible = false;

        if (loggingInResult.IsError)
        {
            await App.Current.MainPage.DisplayAlert("Error", "Something went wrong logging you in. Please try again.", "OK");
            LoginButton.IsEnabled = true;
            LoggingIn.IsVisible = false;
        }
        else // user is logged in
        {
            Preferences.Clear();
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("UserName", loggingInResult.User.Identity.Name);
            await Navigation.PopModalAsync();
        }
    }

    private async Task LogoutAsync()
    {
        LoginButton.IsEnabled = false;
        LoggingIn.IsVisible = true;

        var loggingOutResult = await _authService.LogoutAsync();

        LoggingIn.IsVisible = false;

        if (loggingOutResult.IsError)
        {
            await App.Current.MainPage.DisplayAlert("Error", "Something went wrong logging you out. Please try again.", "OK");
            LoginButton.IsEnabled = true;
            LoggingIn.IsVisible = false;
        }
        else // user is logged out
        {
            Preferences.Clear();
            Preferences.Set("IsLoggedIn", false);
            Preferences.Set("UserName", String.Empty);
            await Navigation.PopModalAsync();
        }
    }
}
