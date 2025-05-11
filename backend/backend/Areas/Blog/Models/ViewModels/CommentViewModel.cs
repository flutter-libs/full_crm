namespace backend.Areas.Blog.Models.ViewModels;

public class AddCommentViewModel
{
    public string AuthorId { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateCommentViewModel
{
    public string Content { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}