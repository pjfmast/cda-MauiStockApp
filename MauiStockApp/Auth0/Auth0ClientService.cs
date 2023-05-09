using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using MauiStockApp.Services;

namespace MauiStockApp.Auth0;
public class Auth0ClientService : IAuthService
{
    private readonly OidcClient oidcClient;
    readonly IPreferences preferences;

    public bool IsAuthenticated => preferences.Get<bool>(nameof(IsAuthenticated), false);

    public string CurrentUser => preferences.Get<string>(nameof(CurrentUser), string.Empty);


    public Auth0ClientService(IPreferences preferences) : this(new Auth0ClientOptions()
    {
        // from Auth-0 blog, but should be stored in secure storage 
        Domain = "dev-lmmvveb5oohd3up5.us.auth0.com",
        ClientId = "rFGaPm2hnt7WpRbvPCiu3FYeTMMdOJjD",
        Scope = "openid profile",
#if WINDOWS
            RedirectUri = "http://localhost/callback"
#else
        RedirectUri = "myapp://callback"
#endif
    })
    {
        this.preferences = preferences;
    }
    private Auth0ClientService(Auth0ClientOptions options)
    {
        oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = $"https://{options.Domain}",
            ClientId = options.ClientId,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = options.Browser
        });
    }

    public IdentityModel.OidcClient.Browser.IBrowser Browser
    {
        get
        {
            return oidcClient.Options.Browser;
        }
        set
        {
            oidcClient.Options.Browser = value;
        }
    }

  
    public async Task<LoginResult> LoginAsync()
    {
        var loginResult = await oidcClient.LoginAsync();
        if (!loginResult.IsError)
        {
            // or use secure storage:
            preferences.Set(nameof(IsAuthenticated), true);
            preferences.Set(nameof(CurrentUser), loginResult.User.Identity.Name);
        }

        return loginResult;
    }

    public async Task<BrowserResult> LogoutAsync()
    {
        var logoutParameters = new Dictionary<string, string>
    {
      {"client_id", oidcClient.Options.ClientId },
      {"returnTo", oidcClient.Options.RedirectUri }
    };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{oidcClient.Options.Authority}/v2/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var logoutResult = await oidcClient.Options.Browser.InvokeAsync(browserOptions);

        if (!logoutResult.IsError)
        {
            // or use secure storage:
            preferences.Set(nameof(IsAuthenticated), false);
            preferences.Set(nameof(CurrentUser), string.Empty);
        }


        return logoutResult;
    }
}
