using BlazorTest.Abstraction;
using BlazorTest.Helpers;
using BlazorTest.Models;
using BlazorTest.Models.Dtos;
using BlazorTest.Pages.Identity;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorTest.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public ApiHelper ApiCaller { get; }
        public ITokenService TokenService { get; }
        public CustomAuthenticationStateProvider CustomAuthenticationStateProvider { get; }

        public AuthService(ApiHelper apiHelper, ITokenService tokenService, CustomAuthenticationStateProvider customAuthenticationStateProvider)
        {
            ApiCaller = apiHelper;
            TokenService = tokenService;
            CustomAuthenticationStateProvider = customAuthenticationStateProvider;
        }


        public async Task Login(string username, string password)
        {
            var token = await ApiCaller.PostAsync<TokenDto>("identity/auth/token",
                new { Username = username, Password = password });

            if (token is not null)
            {
                await TokenService.SetToken(token);
                CustomAuthenticationStateProvider.StateChanged();
            }
        }

        public async Task Logout()
        {
            await TokenService.RemoveToken();
            CustomAuthenticationStateProvider.StateChanged();
        }
    }
}
