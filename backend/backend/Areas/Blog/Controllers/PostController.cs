using backend.Areas.Blog.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Blog.Controllers;

[ApiController]
[Area("Blog")]
[Route("api/[area]/[controller]")]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        return Ok(await _context.Posts.ToListAsync());
    }
}