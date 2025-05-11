using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Ecommerce.Models;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public bool IsActive { get; set; } = true;

    public string ProfileImageUrl { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public virtual IEnumerable<Order>? Orders { get; set; }
    public virtual IEnumerable<Cart>? Carts { get; set; }
    public virtual IEnumerable<Address>? Addresses { get; set; }
    public virtual IEnumerable<Review>? Reviews { get; set; }
    public virtual IEnumerable<WishListItem>? WishListItems { get; set; }
    public virtual IEnumerable<CustomerOrders>? CustomerOrders { get; set; }
}