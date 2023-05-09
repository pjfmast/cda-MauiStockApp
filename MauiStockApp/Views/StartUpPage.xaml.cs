namespace MauiStockApp.Views;
using MauiStockApp.Services;

public partial class StartupPage : ContentPage
{
    private IAuthService _authService;
	public StartupPage(IAuthService authService)
	{
		InitializeComponent();
        _authService = authService;
	}
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (_authService.IsAuthenticated)
        {
            await Shell.Current.GoToAsync(nameof(InputPage));
        } else
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}