using BlazorTest.Abstraction;
using BlazorTest.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorTest.Infrastructure
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ITokenService TokenService { get; }

        public CustomAuthenticationStateProvider(ITokenService tokenService)
        {
            TokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenDTO = await TokenService.GetToken();


            var identity = new ClaimsIdentity();

            if (!(string.IsNullOrEmpty(tokenDTO?.Token) || Convert.ToDateTime(tokenDTO?.ExpireTime) < DateTime.Now))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(tokenDTO.Token), "jwt");
                identity.AddClaim(new Claim(ClaimTypes.Name, tokenDTO.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(',', tokenDTO.Roles)));
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
