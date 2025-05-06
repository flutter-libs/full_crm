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
    private readonly ITaskNotesRepository _taskNotesRepository;
    public TasksController(ITasksRepository tasksRepository, ILogger<TasksController> logger,
        ITaskNotesRepository taskNotesRepository)
    {
        _tasksRepository = tasksRepository;
        _logger = logger;
        _taskNotesRepository = taskNotesRepository;
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

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<TaskNotes>>> GetNotes()
    {
        return Ok(await _taskNotesRepository.GetAllTaskNotesAsync());
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<TaskNotes>> GetNote(int id)
    {
        var note = await _taskNotesRepository.GetTaskNoteById(id);
        return Ok(note);
    }

    [HttpPost("notes")]
    public async Task<ActionResult<TaskNotes>> CreateNote(TaskNotes note)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            await _taskNotesRepository.AddAsync(note);
            return Ok("Note created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult<TaskNotes>> UpdateNote(int id, TaskNotes note)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            await _taskNotesRepository.UpdateAsync(id, note);
            return Ok("Note updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - Exception {id}");
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult<Tasks>> DeleteNote(int id)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _taskNotesRepository.DeleteAsync(id);
        return Ok("Note deleted");
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetCountNotes()
    {
        return Ok(await _taskNotesRepository.CountAsync());
    }
}