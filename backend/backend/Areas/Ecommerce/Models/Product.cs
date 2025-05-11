using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(200)] public string Name { get; set; }

    public string Slug { get; set; }

    public string Description { get; set; }
    [Precision(10,2)]
    public decimal Price { get; set; }
    
    [Precision(10,2)]
    public decimal? SalePrice { get; set; }

    public int StockQuantity { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFeatured { get; set; } = false;

    public string Sku { get; set; }

    public string ThumbnailUrl { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    // Relationships
    public virtual IEnumerable<ProductCategories> ProductCategories { get; set; }
    public virtual IEnumerable<ProductImages> ProductImages { get; set; }
    public virtual IEnumerable<Review> Reviews { get; set; }
    public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public virtual IEnumerable<WishListItem> WishListItems { get; set; }
}