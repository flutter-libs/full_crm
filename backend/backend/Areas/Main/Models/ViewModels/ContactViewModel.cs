using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;


namespace backend.Areas.Main.Models.ViewModels;

public class AddContactViewModel : Contact
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
        
    public DateTime? DateUpdated { get; set; }
    
    [ForeignKey(nameof(OwnerUserId))]
    public string? OwnerUserId { get; set; }
    
    
    public virtual User? OwnerUser { get; set; }
    public IFormFile? ImageUrl { get; set; }
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

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        
    public DateTime? DateUpdated { get; set; }
    [ForeignKey(nameof(OwnerUserId))]
    public string? OwnerUserId { get; set; }
    
    
    public virtual User? OwnerUser { get; set; }
    public IFormFile? ImageUrl { get; set; }
}