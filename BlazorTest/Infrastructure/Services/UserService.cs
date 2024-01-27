using BlazorTest.Abstraction;
using BlazorTest.Helpers;
using BlazorTest.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BlazorTest.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public ApiHelper ApiCaller { get; }

        public UserService(ApiHelper apiCaller)
        {
            ApiCaller = apiCaller;
        }

        public async Task<List<User>?> GetUsers()
        {
            return await ApiCaller.GetAsync<List<User>>("identity/users", setAuthHeader: true);
        }
        public async Task<User?> GetUserById(Guid id)
        {
            return await ApiCaller.GetAsync<User>("identity/users/" + id.ToString());
        }
        public async Task<IEnumerable<UserRole>?> GetUserRoles(Guid userId)
        {
            return await ApiCaller.GetAsync<IEnumerable<UserRole>>("identity/users/" + userId.ToString() + "/roles", setAuthHeader: true);
        }
        public async Task SetUserRoles(Guid userId, Guid[] roleIds)
        {
            await ApiCaller.PutAsync("identity/users/" + userId.ToString() + "/roles", roleIds, setAuthHeader: true);
        }
        public async Task DeleteUser(Guid id)
        {
            await ApiCaller.DeleteAsync("identity/users/" + id.ToString());
        }

        public async Task SaveUser(User user)
        {
            if (user.Id == Guid.Empty)
            {
                var randomString = Guid.NewGuid().ToString();
                var tempPassword = randomString + randomString.ToUpper();
                user.Password = tempPassword;
                await ApiCaller.PostAsync("identity/users", user);
            }
            else
            {
                await ApiCaller.PatchAsync("identity/users/" + user.Id.ToString(), user);
            }
        }

        public async Task SaveMobile(EditMobile mobile)
        {
            await ApiCaller.PatchAsync("identity/users/" + mobile.UserId.ToString() + "/mobile", mobile);
        }

        public async Task SaveEmail(EditEmail email)
        {
            await ApiCaller.PatchAsync("identity/users/" + email.UserId.ToString() + "/email", email);
        }

        public async Task SaveMobileUser(MobileUser user)
        {
            await ApiCaller.PostAsync("identity/mobiles", user);
        }
        public async Task ActivateMobileUser(string mobile, string verificationKey)
        {
            await ApiCaller.PatchAsync("identity/mobiles/" + mobile + "/activation-key/" + verificationKey);
        }
        public async Task MobileVerificationKey(Guid userId)
        {
            await ApiCaller.PostAsync("identity/users/" + userId.ToString() + "/mobile/verification-key",
                new { UserId = userId.ToString() });
        }
        public async Task DeleteMobile(Guid userId)
        {
            await ApiCaller.DeleteAsync("identity/users/" + userId.ToString() + "/mobile",
                new { UserId = userId.ToString() });
        }
        public async Task VerifyMobile(Guid userId, string verificationKey)
        {
            await ApiCaller.PatchAsync("identity/users/" + userId.ToString() + "/mobile/verification-key/" + verificationKey,
                new { UserId = userId.ToString(), VerificationKey = verificationKey });
        }

        public async Task SaveEmailUser(EmailUser user)
        {
            await ApiCaller.PostAsync("identity/emails", user);
        }
        public async Task ActivateEmailUser(string email, string verificationKey)
        {
            await ApiCaller.PatchAsync("identity/emails/" + email + "/activation-key/" + verificationKey);
        }
        public async Task EmailVerificationKey(Guid userId)
        {
            await ApiCaller.PostAsync("identity/users/" + userId.ToString() + "/email/verification-key",
                new { UserId = userId.ToString() });
        }
        public async Task DeleteEmail(Guid userId)
        {
            await ApiCaller.DeleteAsync("identity/users/" + userId.ToString() + "/email",
                new { UserId = userId.ToString() });
        }
        public async Task VerifyEmail(Guid userId, string verificationKey)
        {
            await ApiCaller.PatchAsync("identity/users/" + userId.ToString() + "/email/verification-key/" + verificationKey,
                new { UserId = userId.ToString(), VerificationKey = verificationKey });
        }
        public async Task ClearRoles(Guid userId)
        {
            await ApiCaller.DeleteAsync("identity/users/" + userId.ToString() + "/roles");
        }
    }
}
