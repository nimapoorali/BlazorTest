using System.Security.Claims;

namespace BlazorTest.Models
{
    public class LoginUser
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public int Age { get; set; }
        public List<string> Roles { get; set; } = new();

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
        new (ClaimTypes.Name, Username),
        new (ClaimTypes.Hash, Password),
        new (nameof(Age), Age.ToString())
        }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()),
        "Blazor School"));

        public static LoginUser FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
            Age = Convert.ToInt32(principal.FindFirst(nameof(Age))?.Value),
            Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        };
    }
}
