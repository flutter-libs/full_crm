using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Models;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string ReviewerId { get; set; }
    public User Reviewer { get; set; }
    public string Comment { get; set; }
    [Precision(10,2)]
    public decimal Rating { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}