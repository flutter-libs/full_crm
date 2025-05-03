using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.Identity.Models.ViewModels;

public class RegisterViewModel : IdentityUser
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string UserName { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string City { get; set; }
    [Required] public string State { get; set; }
    [Required] public string ZipCode { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public DateTime DateCreated { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; }
}

public class LoginViewModel : IdentityUser
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class UpdateUserViewModel : IdentityUser
{
    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string City { get; set; }
    [Required] public string State { get; set; }
    [Required] public string ZipCode { get; set; }
    public string? Description { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public DateTime DateCreated { get; set; }
}