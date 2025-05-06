using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksRepository _tasksRepository;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITasksRepository tasksRepository, ILogger<TasksController> logger)
    {
        _tasksRepository = tasksRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
    {
        var tasks = await _tasksRepository.GetAllTasks();
        _logger.LogInformation("Returning tasks");
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tasks>> GetTask(int id)
    {
        var task = await _tasksRepository.GetTaskById(id);
        _logger.LogInformation("Returning task");
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<Tasks>> CreateTask(Tasks task)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            await _tasksRepository.AddAsync(task);
            _logger.LogInformation("Created task");
            return Ok(task);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Tasks>> UpdateTask(int id, Tasks task)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            await _tasksRepository.UpdateAsync(id, task);
            _logger.LogInformation("Updated task");
            return Ok(task);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Task with id {id} failed", ex);
            return BadRequest($"Failed to update Task with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Task with id {id} failed", ex);
            return BadRequest($"Failed to update Task with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Task with id {id} failed", ex);
            return BadRequest($"Failed to update Task with id - Exception {id}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Tasks>> DeleteTask(int id)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            await _tasksRepository.DeleteAsync(id);
            _logger.LogInformation("Deleted task");
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError($"Deleting Task with id {id} failed", e);
            return BadRequest($"Failed to delete Task with id {id}");
        }
    }

    [HttpGet("countTasks")]
    public async Task<ActionResult<int>> GetCountTasks()
    {
        var tasks = await _tasksRepository.CountAsync();
        return Ok(tasks);
    }
}