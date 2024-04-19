using BlazorTest.Models;
using static MudBlazor.Icons;

namespace BlazorTest.Abstraction
{
    public interface IUserService
    {
        Task<List<User>?> GetUsers();
        Task<User?> GetUserById(Guid id);
        Task<IEnumerable<UserRole>?> GetUserRoles(Guid userId);
        Task SetUserRoles(Guid userId, Guid[] roleIds);
        Task SaveUser(User user);
        Task SaveMobileUser(MobileUser user);
        Task SaveMobile(EditMobile mobile);
        Task VerifyMobile(Guid userId, string verificationKey);
        Task MobileVerificationKey(Guid userId);
        Task DeleteMobile(Guid userId);
        Task ActivateMobileUser(string mobile, string verificationKey);
        Task ActivateEmailUser(string email, string verificationKey);
        Task SaveEmail(EditEmail email);
        Task VerifyEmail(Guid userId, string verificationKey);
        Task EmailVerificationKey(Guid userId);
        Task DeleteEmail(Guid userId);
        Task SaveEmailUser(EmailUser user);
        Task DeleteUser(Guid id);
        Task ClearRoles(Guid userId);
    }
}
