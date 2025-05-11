using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Models;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    public virtual Order Order { get; set; }

    [Required]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Precision(10,2)]
    public decimal UnitPrice { get; set; }
    [Precision(10,2)]

    public decimal TotalPrice => UnitPrice * Quantity;
}