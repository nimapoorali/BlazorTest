using BlazorTest.Abstraction;
using BlazorTest.Models;
using BlazorTest.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace BlazorTest.Helpers
{
    public class AuthorizeService
    {
        public AuthenticationDataMemoryStorage AuthenticationDataMemoryStorage { get; }
        public IAuthService AuthService { get; }

        public AuthorizeService(AuthenticationDataMemoryStorage authenticationDataMemoryStorage, IAuthService authService)
        {
            AuthenticationDataMemoryStorage = authenticationDataMemoryStorage;
            AuthService = authService;
        }

        public async Task<ApplicationUser?> SendAuthenticateRequestAsync(string username, string password)
        {
            //var token = await AuthService.GetToken(username, password);
            var token = new TokenDto();

            if (token is not null)
            {
                //var claimPrincipal = CreateClaimsPrincipalFromToken(token.token);
                //var appUser = ApplicationUser.FromClaimsPrincipal(claimPrincipal);
                var appUser = FetchUserFromBrowser();
                
                PersistUserToBrowser(token.Token);

                return appUser;
            }

            return null;
        }

        public ApplicationUser? FetchUserFromBrowser()
        {
            var claimsPrincipal = CreateClaimsPrincipalFromToken(AuthenticationDataMemoryStorage.Token);
            var appUser = ApplicationUser.FromClaimsPrincipal(claimsPrincipal);

            return appUser;
        }

        private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new(jwtSecurityToken.Claims, "Identity Authentication");
            }

            return new(identity);
        }

        private void PersistUserToBrowser(string token) => AuthenticationDataMemoryStorage.Token = token;


    }
}
