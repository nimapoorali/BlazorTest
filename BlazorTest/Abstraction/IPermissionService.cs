using BlazorTest.Models;
using static MudBlazor.Icons;

namespace BlazorTest.Abstraction
{
    public interface IPermissionService
    {
        Task<List<Permission>?> GetPermissions();
        Task<Permission?> GetPermissionById(Guid id);
        Task<IEnumerable<PermissionRole>?> GetPermissionRoles(Guid permissionId);
        Task SetPermissionRoles(Guid permissionId, Guid[] roleIds);
        Task SavePermission(Permission permission);
        Task DeletePermission(Guid id);
        Task ClearRoles(Guid permissionId);
    }
}
