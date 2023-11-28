using BlazorTest.Models;
using static MudBlazor.Icons;

namespace BlazorTest.Abstraction
{
    public interface IRoleService
    {
        Task<List<Role>?> GetRoles();
        Task<Role?> GetRoleById(Guid id);
        Task SaveRole(Role role);
        Task DeleteRole(Guid id);
    }
}
