using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;

namespace MauiStockApp.Services;

public interface IAuthService
{
    Task<LoginResult> LoginAsync();
    Task<BrowserResult> LogoutAsync();
    bool IsAuthenticated { get; }
    string CurrentUser { get; }

    IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
}
