using BlazorTest.Abstraction;
using BlazorTest.Helpers;
using BlazorTest.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BlazorTest.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        public ApiHelper ApiCaller { get; }

        public PermissionService(ApiHelper apiCaller)
        {
            ApiCaller = apiCaller;
        }

        public async Task<List<Permission>?> GetPermissions()
        {
            return await ApiCaller.GetAsync<List<Permission>>("identity/permissions");
        }
        public async Task<Permission?> GetPermissionById(Guid id)
        {
            return await ApiCaller.GetAsync<Permission>("identity/permissions/" + id.ToString());
        }
        public async Task<IEnumerable<PermissionRole>?> GetPermissionRoles(Guid permissionId)
        {
            return await ApiCaller.GetAsync<IEnumerable<PermissionRole>>("identity/permissions/" + permissionId.ToString() + "/roles");
        }
        public async Task SetPermissionRoles(Guid permissionId, Guid[] roleIds)
        {
            await ApiCaller.PutAsync("identity/permissions/" + permissionId.ToString() + "/roles", roleIds);
        }
        public async Task DeletePermission(Guid id)
        {
            await ApiCaller.DeleteAsync("identity/permissions/" + id.ToString());
        }

        public async Task ClearRoles(Guid permissionId)
        {
            await ApiCaller.DeleteAsync("identity/permissions/" + permissionId.ToString() + "/roles");
        }

        public async Task SavePermission(Permission permission)
        {
            if (permission.Id == Guid.Empty)
            {
                await ApiCaller.PostAsync("identity/permissions", permission);
            }
            else
            {
                await ApiCaller.PutAsync("identity/permissions/" + permission.Id.ToString(), permission);
            }
        }
    }
}
