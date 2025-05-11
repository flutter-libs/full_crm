using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Ecommerce.Models;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer Customer { get; set; }

    [MaxLength(100)]
    public string SessionId { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; }
}