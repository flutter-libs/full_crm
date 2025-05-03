using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;
    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("error-development")]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            return NotFound("Environment not development");
        }
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message
        );
    }

    [HttpGet("/error")]
    public IActionResult HandleError() => Problem();
}