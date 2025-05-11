using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Ecommerce.Controllers;

[ApiController]
[Area("Ecommerce")]
[Route("api/[area]/[controller]")]
public class CartController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CartController> _logger;

    public CartController(ApplicationDbContext context, ILogger<CartController> logger)
    {
        _context = context;
        _logger = logger;
    }
}