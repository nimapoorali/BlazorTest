using BlazorTest.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorTest.Infrastructure
{
    public class CustomAuthenticationStateProviderObsolete : AuthenticationStateProvider
    {
        public AuthorizeService AuthorizeService { get; }

        public CustomAuthenticationStateProviderObsolete(AuthorizeService authorizeService)
        {
            AuthorizeService = authorizeService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = AuthorizeService.FetchUserFromBrowser();

            if (user is not null)
            {
                var authenticatedUser = await AuthorizeService.SendAuthenticateRequestAsync(user.Username, "user.Password");

                if (authenticatedUser is not null)
                {
                    principal = authenticatedUser.ToClaimsPrincipal();
                }
            }

            return new(principal);
        }

        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await AuthorizeService.SendAuthenticateRequestAsync(username, password);

            if (user is not null)
            {
                principal = user.ToClaimsPrincipal();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
    }
}
