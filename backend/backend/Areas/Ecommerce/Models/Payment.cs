using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Ecommerce.Models;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    public virtual Order Order { get; set; }

    [Required]
    [MaxLength(100)]
    public string PaymentMethod { get; set; }

    [MaxLength(100)]
    public string Provider { get; set; }

    [MaxLength(200)]
    public string TransactionId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime? ProcessedAt { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; }
}