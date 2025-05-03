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
public class UserRolesViewModel : IdentityUserRole<string>
{
    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; }
    public User User {get;set;}
    [ForeignKey(nameof(RoleId))]
    public string RoleId { get; set; }
    public Role Role { get; set; }
}
public class RoleAssignViewModel
{
    
    public string RoleID { get; set; }
    public string? RoleName { get; set; }
    public bool RoleExist { get; set; }
}