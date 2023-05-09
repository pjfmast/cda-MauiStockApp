using MauiStockApp.Auth0;
using MauiStockApp.Helpers;

namespace MauiStockApp;

public partial class App : Application
{
    public static Theme Theme { get; set; } = Theme.Default;

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell(new Auth0ClientService(Preferences.Default));
	}

    // See Maui in Action Listing 8.6
    // OnStart is a lifecycle method

    // move logic to a StartUpPage

    //  be cautious if you use async / await in these methods since the app will continue to load in the mean time 
    //protected override async void OnStart()
    //{
    //    base.OnStart();
    //    bool isLoggedIn = Preferences.Get("IsLoggedIn", false);

    //    if (!isLoggedIn)
    //    {
    //        // Here plugin PageResolver is applied:
    //        await MainPage.Navigation.PushModalAsync<LoginPage>();
    //    }
    //}
}
