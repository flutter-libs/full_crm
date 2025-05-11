using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Ecommerce.Models;

public class CustomerOrders
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}