using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;

namespace MauiStockApp.Services
{
    public class MockAuthService : IAuthService
    {
        public IdentityModel.OidcClient.Browser.IBrowser Browser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        bool IAuthService.IsAuthenticated => true;

        string IAuthService.CurrentUser => "Henk";


        public Task<LoginResult> LoginAsync()
        => Task.FromResult(new IdentityModel.OidcClient.LoginResult());
        

        public Task<BrowserResult> LogoutAsync()
         => Task.FromResult(new BrowserResult());
    }
}
