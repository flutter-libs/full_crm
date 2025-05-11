using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.Identity.Models.ViewModels;

public class AddRoleViewModel : IdentityRole
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
public class UserRolesViewModel
{
    public string UserId { get; set; }
    public string RoleId { get; set; }
}
public class RoleAssignViewModel
{
    
    public string RoleID { get; set; }
    public string? RoleName { get; set; }
    public bool RoleExist { get; set; }
}