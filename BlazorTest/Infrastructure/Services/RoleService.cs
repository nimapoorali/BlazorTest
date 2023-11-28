using BlazorTest.Abstraction;
using BlazorTest.Helpers;
using BlazorTest.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BlazorTest.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        public ApiHelper ApiCaller { get; }

        public RoleService(ApiHelper apiCaller)
        {
            ApiCaller = apiCaller;
        }

        public async Task<List<Role>?> GetRoles()
        {
            return await ApiCaller.GetAsync<List<Role>>("identity/roles");
        }
        public async Task<Role?> GetRoleById(Guid id)
        {
            return await ApiCaller.GetAsync<Role>("identity/roles/" + id.ToString());
        }
        public async Task DeleteRole(Guid id)
        {
            await ApiCaller.DeleteAsync("identity/roles/" + id.ToString());
        }

        public async Task SaveRole(Role role)
        {
            if (role.Id == Guid.Empty)
            {
                await ApiCaller.PostAsync("identity/roles", role);
            }
            else
            {
                await ApiCaller.PutAsync("identity/roles/" + role.Id.ToString(), role);
            }
        }
    }
}
