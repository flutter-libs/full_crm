using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class JobTaskRepository : IJobTaskRepository
{
    private readonly ApplicationDbContext _context;
    public JobTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<JobTask>> GetJobTasks()
    {
        return await _context.JobTasks
            .Include(j => j.Job)
            .Include(c => c.Tasks)
            .ToListAsync();
    }

    public async Task<JobTask> GetJobTask(int id)
    {
        var jobTasks =  await _context.JobTasks
            .Include(j => j.Job)
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(j => j.Id == id);
        if (jobTasks == null)
        {
            throw new Exception($"Job task with id {id} not found");
        }
        return jobTasks;
    }

    public async Task<JobTask> CreateJobTask(AddJobTaskViewModel jobTask)
    {
        var createJobTask = new JobTask
        {
            Tasks = new Tasks
            {
                TaskDescription = jobTask.Tasks.TaskDescription,
                DueDate = jobTask.Tasks.DueDate,
                Status = jobTask.Tasks.Status,
                Priority = jobTask.Tasks.Priority,
                AssignedToUser = jobTask.Tasks.AssignedToUser,
            },
            JobId = jobTask.JobId,
            Created = jobTask.Created,
        };
        _context.JobTasks.Add(createJobTask);
        await _context.SaveChangesAsync();
        return createJobTask;
    }

    public async Task<JobTask> UpdateJobTask(int id, UpdateJobTaskViewModel jobTask)
    {
        var updateJobTask = await GetJobTask(id);
        updateJobTask.Tasks.DueDate = jobTask.Tasks.DueDate;
        updateJobTask.Tasks.TaskDescription = jobTask.Tasks.TaskDescription;
        updateJobTask.Tasks.Priority = jobTask.Tasks.Priority;
        updateJobTask.Tasks.AssignedToUser = jobTask.Tasks.AssignedToUser;
        updateJobTask.JobId = jobTask.JobId;
        updateJobTask.Tasks.Status = jobTask.Tasks.Status;
        updateJobTask.Updated = jobTask.Updated;
        _context.Update(updateJobTask);
        await _context.SaveChangesAsync();
        return updateJobTask;
    }

    public async Task DeleteJobTask(int id)
    {
        var jobTask = await GetJobTask(id);
        _context.JobTasks.Remove(jobTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountJobTasks()
    {
        return await _context.JobTasks.CountAsync();
    }
}