using Blazored.LocalStorage;
using BlazorTest.Abstraction;
using BlazorTest.Models.Dtos;

namespace BlazorTest.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public ILocalStorageService LocalStorageService { get; }

        public TokenService(ILocalStorageService localStorageService)
        {
            LocalStorageService = localStorageService;
        }


        public async Task SetToken(TokenDto tokenDTO)
        {
            await LocalStorageService.SetItemAsync("token", tokenDTO);
        }

        public async Task<TokenDto> GetToken()
        {
            return await LocalStorageService.GetItemAsync<TokenDto>("token");
        }

        public async Task RemoveToken()
        {
            await LocalStorageService.RemoveItemAsync("token");
        }
    }
}
