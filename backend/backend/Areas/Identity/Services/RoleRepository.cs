using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Services;

public class RoleRepository : IRoleRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _context;

    public RoleRepository(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles;
    }
    
        public async Task<IdentityResult> CreateRoleAsync([FromBody] AddRoleViewModel model)
        {
            if (await _roleManager.RoleExistsAsync(model.Name))
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });

            var role = new Role
            {
                Name = model.Name,
                Description = model.Description,
                NormalizedName = model.Name.ToUpper()
            };
            return await _roleManager.CreateAsync(role);
        }
        

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
}