using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Areas.Identity.Services;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Identity.Controllers;

[ApiController]
[Area("Identity")]
[Route("api/[area]/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IRoleRepository roleRepository, ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager,
        ILogger<RoleController> logger)
    {
        _roleRepository = roleRepository;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpPost]
    [Route("assign-role")]
    public async Task<ActionResult> AssignRole(string roleId, string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                Console.WriteLine("Role not found");
                return NotFound("Role doesn't exist.");
            }
            if (user == null)
            {
                Console.WriteLine("User not found");
                return NotFound("User not found.");
            }

            var userRoles = new UserRoles
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _context.UserRoles.Add(userRoles);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Role assigned" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(new {Message = "User could not be assigned to role."});
        }
    }
    

    [HttpGet("roles/{userId}")]
    public async Task<ActionResult> GetUserRoles(string userId)
    {
        var roles = await _roleRepository.GetUserRolesAsync(userId);
        return Ok(roles);
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([FromBody] AddRoleViewModel model)
    {
        var role = await _roleRepository.CreateRoleAsync(model);
        return Ok(role);
    }
}