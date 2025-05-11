using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Ecommerce.Models;

public class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
    public string ThumbnailAltText { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public IEnumerable<ProductImages> ProductImages { get; set; }
}