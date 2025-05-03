using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Controllers;


[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class JobController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<JobController> _logger;
    public JobController(ApplicationDbContext context, IJobRepository jobRepository, ILogger<JobController> logger)
    {
        _context = context;
        _jobRepository = jobRepository;
        _logger = logger;
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
}