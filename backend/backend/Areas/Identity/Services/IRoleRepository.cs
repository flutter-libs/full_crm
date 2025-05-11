using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Identity.Services;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<IdentityResult> CreateRoleAsync([FromBody] AddRoleViewModel model);
}