using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Blog.Models;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Content { get; set; }
    public string AuthorId { get; set; }
    public User Author { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}