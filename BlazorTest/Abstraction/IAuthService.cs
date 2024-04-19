using BlazorTest.Models;

namespace BlazorTest.Abstraction
{
    public interface IAuthService
    {
        Task Login(string username, string password);
        Task Logout();
    }
}
