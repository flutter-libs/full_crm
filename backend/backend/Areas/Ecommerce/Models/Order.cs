using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public DateTime? ShippedDate { get; set; }
    
    [MaxLength(100)]
    public string Status { get; set; } = "Pending";

    [Required]
    [Precision(10,2)]
    public decimal TotalAmount { get; set; }
    [Precision(10,2)]
    public decimal? Discount { get; set; }

    public string PaymentMethod { get; set; }

    public string PaymentStatus { get; set; }

    public string TransactionId { get; set; }
    
    [MaxLength(200)]
    public string ShippingFullName { get; set; }

    [MaxLength(200)]
    public string ShippingAddress { get; set; }

    [MaxLength(100)]
    public string ShippingCity { get; set; }

    [MaxLength(100)]
    public string ShippingState { get; set; }

    [MaxLength(20)]
    public string ShippingZipCode { get; set; }

    [MaxLength(100)]
    public string ShippingCountry { get; set; }

    public string ShippingMethod { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; }
    public IEnumerable<Payment> Payments { get; set; }
    public IEnumerable<CustomerOrders> CustomerOrders { get; set; }
}