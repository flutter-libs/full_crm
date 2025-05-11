using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Ecommerce.Models;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    [Required]
    [MaxLength(200)]
    public string Street { get; set; }

    [Required]
    [MaxLength(100)]
    public string City { get; set; }

    [Required]
    [MaxLength(100)]
    public string State { get; set; }

    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; }

    [Required]
    [MaxLength(100)]
    public string Country { get; set; }

    public bool IsDefaultShipping { get; set; } = false;
    public bool IsDefaultBilling { get; set; } = false;
}