using backend.Areas.Utility.Models;
using backend.Areas.Utility.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Utility.Controllers;

[ApiController]
[Area("Utility")]
[Route("api/[area]/[controller]")]
public class AnalyticController : ControllerBase
{
    private readonly IAnalyticRepository _repository;
    private readonly ILogger<AnalyticController> _logger;

    public AnalyticController(IAnalyticRepository repository, ILogger<AnalyticController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // Get all analytics
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Analytic>>> GetAll()
    {
        var analytics = await _repository.GetAllAsync();
        return Ok(analytics);
    }

    // Get analytic by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Analytic>> Get(int id)
    {
        var analytic = await _repository.GetByIdAsync(id);
        if (analytic == null)
            return NotFound();
        return Ok(analytic);
    }

    // Create a new analytic record
    [HttpPost]
    public async Task<ActionResult<Analytic>> Create(Analytic analytic)
    {
        var createdAnalytic = await _repository.AddAsync(analytic);
        return CreatedAtAction(nameof(Get), new { id = createdAnalytic.Id }, createdAnalytic);
    }

    // Update an existing analytic record
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Analytic analytic)
    {
        if (id != analytic.Id)
            return BadRequest();

        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.UpdateAsync(analytic);
        return NoContent();
    }

    // Delete an analytic record
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}