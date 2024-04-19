using BlazorTest.Models;
using BlazorTest.Pages.Identity;
using MudBlazor;
using System.Security.Claims;

namespace BlazorTest.Helpers
{
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string[] Roles { get; set; }

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
        new (ClaimTypes.Name, Username),
        //new (ClaimTypes.Hash, Password),
        //new (nameof(Age), Age.ToString())
        }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()),
        "Identity Authentication"));

        public static ApplicationUser FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            //Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
            //Age = Convert.ToInt32(principal.FindFirst(nameof(Age))?.Value),
            Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray()
        };
    }
}
