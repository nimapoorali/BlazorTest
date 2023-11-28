using BlazorTest.Models.Dtos;

namespace BlazorTest.Abstraction
{
    public interface ITokenService
    {
        Task<TokenDto> GetToken();
        Task RemoveToken();
        Task SetToken(TokenDto tokenDTO);
    }
}
