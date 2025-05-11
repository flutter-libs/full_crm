using backend.Areas.Ecommerce.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Controllers;

[ApiController]
[Area("Ecommerce")]
[Route("api/[area]/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OrderController> _logger;

    public OrderController(ApplicationDbContext context, ILogger<OrderController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return Ok(await _context.Orders.ToListAsync());
    }
}