using backend.Areas.Ecommerce.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Ecommerce.Controllers;

[ApiController]
[Area("Ecommerce")]
[Route("api/[area]/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ApplicationDbContext context, ILogger<CustomerController> logger)
    {
        _context = context;
        _logger = logger;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return Ok(await _context.Customers.ToListAsync());
    }
}