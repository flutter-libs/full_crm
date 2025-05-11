using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Blog.Controllers;

[ApiController]
[Area("Blog")]
[Route("api/[area]/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CommentController(ApplicationDbContext context)
    {
        _context = context;
    }
}