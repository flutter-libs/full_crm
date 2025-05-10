using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;


[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class JobController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<JobController> _logger;
    private readonly IJobNoteRepository _jobNoteRepository;
    private readonly IJobTaskRepository _jobTaskRepository;
    public JobController(ApplicationDbContext context, IJobRepository jobRepository, ILogger<JobController> logger,
        IJobNoteRepository jobNoteRepository, IJobTaskRepository jobTaskRepository)
    {
        _context = context;
        _jobRepository = jobRepository;
        _logger = logger;
        _jobNoteRepository = jobNoteRepository;
        _jobTaskRepository = jobTaskRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
    {
        return Ok(await _jobRepository.GetAllJobsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Job>> GetJob(int id)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);
        if (job == null)
        {
            return NotFound("Job not found");
        }
        return Ok(job);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Job>> UpdateJob(int id, Job job)
    {
        var jobToUpdate = await _jobRepository.GetJobByIdAsync(id);
        if (jobToUpdate == null)
        {
            return NotFound("Job not found");
        }

        return Ok(await _jobRepository.UpdateJobAsync(id, jobToUpdate));
    }

    [HttpPost]
    public async Task<ActionResult<Job>> CreateJob(Job job)
    {
        var jobToCreate = await _jobRepository.CreateJobAsync(job);
        return Ok(jobToCreate);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Job>> DeleteJob(int id)
    {
        var jobToDelete = await _jobRepository.GetJobByIdAsync(id);
        if (jobToDelete == null)
        {
            return NotFound("Job not found");
        }
        return Ok(await _jobRepository.DeleteJobAsync(id));
    }

    [HttpGet("jobCount")]
    public async Task<ActionResult<int>> GetJobCount()
    {
        return Ok(await _jobRepository.CountAllJobsAsync());
    }

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<JobNotes>>> JobNotes()
    {
        return Ok(await _jobNoteRepository.GetAllJobNotesAsync());
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<JobNotes>> GetJobNotes(int id)
    {
        var jobNote = await _jobNoteRepository.GetJobNoteById(id);
        return Ok(jobNote);
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult<JobNotes>> UpdateJobNotes(int id, JobNotes jobNote)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _jobNoteRepository.UpdateAsync(id, jobNote);
            return Ok("Job Notes updated.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Job Note with id {id} failed", ex);
            return BadRequest($"Failed to update Job Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Job Note with id {id} failed", ex);
            return BadRequest($"Failed to update Job Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Job Note with id {id} failed", ex);
            return BadRequest($"Failed to update Job Note with id - Exception {id}");
        }
    }

    [HttpPost("notes")]
    public async Task<ActionResult<JobNotes>> CreateJobNotes(JobNotes jobNote)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _jobNoteRepository.AddAsync(jobNote);
            return Ok("Job Notes created.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult<JobNotes>> DeleteJobNotes(int id)
    {
        await _jobNoteRepository.DeleteAsync(id);
        return Ok("Job Notes deleted.");
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetNotesCount()
    {
        return Ok(await _jobNoteRepository.CountAsync());
    }

    [HttpGet("jobTasks")]
    public async Task<ActionResult<IEnumerable<JobTask>>> GetJobTasks()
    {
        try
        {
            return Ok(await _jobTaskRepository.GetJobTasks());
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception at retrieving job tasks {ex.Message}");
        }
    }

    [HttpGet("jobTasks/{id}")]
    public async Task<ActionResult<JobTask>> GetJobTask(int id)
    {
        try
        {
            return Ok(await _jobTaskRepository.GetJobTask(id));
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception at retrieving job tasks with the id: {id} - {ex.Message}");
        }
    }

    [HttpPost("jobTasks")]
    public async Task<ActionResult<JobTask>> CreateJobTask([FromBody] AddJobTaskViewModel jobTask)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _jobTaskRepository.CreateJobTask(jobTask);
            return Ok("Job Task Created.");
        }
        catch (Exception ex)
        {
            return BadRequest($"There was an Exception when creating a job task - {ex.Message}");
        }
    }

    [HttpPut("jobTasks/{id}")]
    public async Task<ActionResult<JobTask>> UpdateJobTask(int id, [FromBody] UpdateJobTaskViewModel jobTask)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _jobTaskRepository.UpdateJobTask(id, jobTask);
            return Ok("Job Task Updated.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest($"DbUpdateConcurrencyException: {ex.Message}");
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"DbUpdateException: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
    }

    [HttpDelete("jobTasks/{id}")]
    public async Task<ActionResult<JobTask>> DeleteJobTask(int id)
    {
        await _jobTaskRepository.DeleteJobTask(id);
        return Ok("Job Task Deleted.");
    }

    [HttpGet("jobTasks/count")]
    public async Task<ActionResult<int>> GetJobTasksCount()
    {
        return Ok(await _jobTaskRepository.CountJobTasks());
    }
}