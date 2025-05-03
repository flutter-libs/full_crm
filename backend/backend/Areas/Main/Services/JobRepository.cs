using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;

    public JobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Job>> GetAllJobsAsync()
    {
        return await _context.Jobs
            .Include(j => j.Contact)
            .Include(j => j.AssignedUser)
            .Include(j => j.CreatedByUser)
            .ToListAsync();
    }

    public async Task<Job?> GetJobByIdAsync(int id)
    {
        return await _context.Jobs
            .Include(j => j.Contact)
            .Include(j => j.AssignedUser)
            .Include(j => j.CreatedByUser)
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<Job> CreateJobAsync(Job job)
    {
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<Job?> UpdateJobAsync(int id, Job updatedJob)
    {
        var existingJob = await _context.Jobs.FindAsync(id);
        if (existingJob == null)
            return null;

        // Map updated fields
        _context.Entry(existingJob).CurrentValues.SetValues(updatedJob);
        await _context.SaveChangesAsync();
        return existingJob;
    }

    public async Task<bool> DeleteJobAsync(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null)
            return false;

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> CountAllJobsAsync()
    {
        return await _context.Jobs.CountAsync();
    }
}