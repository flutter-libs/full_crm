using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Blog.Models;

public class PostCategories
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int CategoryId { get; set; }
    public PostCategory PostCategory { get; set; }
}