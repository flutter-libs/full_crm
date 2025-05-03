using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Identity.Services;

public interface IUserRepository
{
    Task<IdentityResult> RegisterAsync([FromBody] RegisterViewModel model);
    Task<string?> LoginAsync([FromBody] LoginViewModel model);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(string userId);
    Task<IdentityResult> UpdateUserAsync([FromBody] UpdateUserViewModel model);
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IdentityResult> AssignRoleAsync(string userId, string roleName);
    Task<int> CountUsersAsync();
}