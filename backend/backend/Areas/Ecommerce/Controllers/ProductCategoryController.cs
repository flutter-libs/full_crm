using backend.Areas.Ecommerce.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Controllers;

[ApiController]
[Area("Ecommerce")]
[Route("api/[area]/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductCategoryController> _logger;

    public ProductCategoryController(ApplicationDbContext context, ILogger<ProductCategoryController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
    {
        return Ok(await _context.ProductCategory.ToListAsync());
    }
}