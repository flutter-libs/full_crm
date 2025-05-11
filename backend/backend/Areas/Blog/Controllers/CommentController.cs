using backend.Areas.Blog.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return Ok(await _context.Comments.ToListAsync());
    }
}