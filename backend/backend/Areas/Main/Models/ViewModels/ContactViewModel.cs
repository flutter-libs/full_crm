using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;


namespace backend.Areas.Main.Models.ViewModels;

public class AddContactViewModel
{
    [StringLength(100)]
    public string FirstName { get; set; }
    
    [StringLength(100)]
    public string LastName { get; set; }

    [StringLength(150)]
    public string CompanyName { get; set; }

    [StringLength(150)]
    public string JobTitle { get; set; }

    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; }

    [Phone]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(200)]
    public string AddressLine1 { get; set; }

    [StringLength(200)]
    public string AddressLine2 { get; set; }

    [StringLength(100)]
    public string City { get; set; }

    [StringLength(100)]
    public string State { get; set; }

    [StringLength(20)]
    public string ZipCode { get; set; }

    [StringLength(100)]
    public string Country { get; set; }

    public string Notes { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    public string? OwnerUserId { get; set; }

    public string? ImageUrl { get; set; }
}

public class UpdateContactViewModel : Contact
{
    [StringLength(100)]
    public string FirstName { get; set; }
    
    [StringLength(100)]
    public string LastName { get; set; }

    [StringLength(150)]
    public string CompanyName { get; set; }

    [StringLength(150)]
    public string JobTitle { get; set; }

    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; }

    [Phone]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(200)]
    public string AddressLine1 { get; set; }

    [StringLength(200)]
    public string AddressLine2 { get; set; }

    [StringLength(100)]
    public string City { get; set; }

    [StringLength(100)]
    public string State { get; set; }

    [StringLength(20)]
    public string ZipCode { get; set; }

    [StringLength(100)]
    public string Country { get; set; }

    public string Notes { get; set; }

    public DateTime? DateUpdated { get; set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
}

public class AddContactNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public int ContactId { get; set; }
}

public class UpdateContactNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}