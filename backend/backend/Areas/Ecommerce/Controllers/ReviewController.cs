using backend.Areas.Ecommerce.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Controllers;

[ApiController]
[Area("Ecommerce")]
[Route("api/[area]/[controller]")]
public class ReviewController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(ApplicationDbContext context, ILogger<ReviewController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        return Ok(await _context.Reviews.ToListAsync());
    }
}