using MauiStockApp.Services;
using MauiStockApp.ViewModels;
using MauiStockApp.Views;
using Microsoft.Extensions.Logging;
using MauiStockApp.Auth0;
using CommunityToolkit.Maui;

namespace MauiStockApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit()
            .UseMauiMaps();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IPreferences>(Preferences.Default);
        //builder.Services.AddSingleton<IAuthService, MockAuthService>();
        builder.Services.AddSingleton<IAuthService, Auth0ClientService>();

        builder.Services.AddSingleton<IProductService, MockProductService>();
        builder.Services.AddSingleton<IInventoryService, MockInventoryService>();
        builder.Services.AddSingleton<IManufactorerService, MockManufactorerService>();

        builder.Services.AddTransient<StartupPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<InputPage>();
        builder.Services.AddTransient<ReportPage>();
        builder.Services.AddTransient<MapPage>();

        builder.Services.AddTransient<InputViewModel>();
        builder.Services.AddTransient<MapViewModel>();

		// see after listing 10.16
        builder.Services.AddTransient<ReportViewModel>();


        return builder.Build();
	}
}
