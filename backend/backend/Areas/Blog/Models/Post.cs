using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Blog.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageUrlTwo  { get; set; }
    public string? ImageUrlThree { get; set; }
    public string? ImageUrlFour { get; set; }
    public string? ImageUrlFive { get; set; }
    public string AuthorId { get; set; }
    public User Author { get; set; }
    public IEnumerable<PostCategories> PostCategories { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}