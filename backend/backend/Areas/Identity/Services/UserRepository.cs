using backend.Areas.Identity.Models;
using backend.Areas.Identity.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Identity.Services;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager,
        RoleManager<Role> roleManager, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterAsync([FromBody] RegisterViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            UserName = model.UserName,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            City = model.City,
            State = model.State,
            ZipCode = model.ZipCode,
            DateOfBirth = model.DateOfBirth,
            DateCreated = DateTime.Now
        };
        
        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<string?> LoginAsync([FromBody] LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return null!;

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
            return null!;

        // Optional: generate a JWT token (simple example)
        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        // This is a *basic* JWT creation example.
        // You can customize claims, expiry, secret key, etc.
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var key = System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email!)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users =  await _context.Users
            .Include(x => x.UserRoles)!
            .ThenInclude(xu => xu.Role)
            .ToListAsync();
        return users;
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var user =  await _context.Users
            .Include(x => x.UserRoles)!
            .ThenInclude(xu => xu.Role)
            .FirstOrDefaultAsync(xc => xc.Id == userId);
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }
        return user;
    }

    public async Task<IdentityResult> UpdateUserAsync(UpdateUserViewModel model)
    {
        var existingUser = await _userManager.FindByIdAsync(model.Id);
        if (existingUser == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        existingUser.Name = model.Name;
        existingUser.Email = model.Email;
        existingUser.UserName = model.UserName;
        existingUser.PhoneNumber = model.PhoneNumber;
        existingUser.Address = model.Address;
        existingUser.City = model.City;
        existingUser.State = model.State;
        existingUser.ZipCode = model.ZipCode;
        existingUser.DateOfBirth = model.DateOfBirth;
        existingUser.DateCreated = DateTime.Now;
        // add more fields if needed

        return await _userManager.UpdateAsync(existingUser);
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> AssignRoleAsync(string userId, string roleName)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountUsersAsync()
    {
        throw new NotImplementedException();
    }
}