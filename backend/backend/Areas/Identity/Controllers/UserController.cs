using System.Security.Claims;
using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Areas.Identity.Services;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Controllers;

[ApiController]
[Area("Identity")]
[Route("api/[area]/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<UserController> _logger;
    
    public UserController(ApplicationDbContext context, IUserRepository userRepository, UserManager<User> userManager, IRoleRepository roleRepository, 
        ILogger<UserController> logger)
    {
        _context = context;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _logger = logger;
    }

    [HttpGet("current-user")]
    public async Task<ActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Unauthorized();
        }
        return Ok(new
        {
            user.Id,
            user.Email,
            user.UserName,
            user.Name,
            user.PhoneNumber,
            user.Address,
            user.City,
            user.State,
            user.ZipCode,
            user.DateOfBirth,
            user.Description
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        var result = await _userRepository.RegisterAsync(model);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        var token = await _userRepository.LoginAsync(model);
        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel model)
    {
        try
        {
            var result = await _userRepository.UpdateUserAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User updated successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new Exception("Failed to update user - DbUpdateConcurrencyException: " + ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Failed to update user - DbUpdateException: " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update user - Exception: " + ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _userRepository.DeleteUserAsync(id);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User deleted successfully");
    }
}