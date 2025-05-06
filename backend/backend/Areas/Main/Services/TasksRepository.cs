using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class TasksRepository : ITasksRepository
{
    private readonly ApplicationDbContext _context;
    public TasksRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Tasks?>> GetAllTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<Tasks?> GetTaskById(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<Tasks> AddAsync(Tasks task)
    {
        var tasks = new Tasks
        {
            TaskDescription = task.TaskDescription,
            DueDate = task.DueDate,
            Status = task.Status,
            Priority = task.Priority,
            AssignedToUserId = task.AssignedToUserId,
            CampaignId = task.CampaignId,
            JobId = task.JobId,
            ContactId = task.ContactId,
            DateCreated = DateTime.Now
        };
        _context.Tasks.Add(tasks);
        await _context.SaveChangesAsync();
        return tasks;
    }

    public async Task UpdateAsync(int id, Tasks task)
    {
        var tasks = await _context.Tasks.FindAsync(id);
        tasks!.TaskDescription = task.TaskDescription;
        tasks!.DueDate = task.DueDate;
        tasks!.Status = task.Status;
        tasks!.Priority = task.Priority;
        tasks!.AssignedToUserId = task.AssignedToUserId;
        tasks!.DateUpdated = task.DateUpdated;
        tasks!.DateCompleted = task.DateCompleted;
        _context.Tasks.Update(tasks);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tasks = await _context.Tasks.FindAsync(id);
        if (tasks == null) throw new NullReferenceException("Task not found");
        _context.Tasks.Remove(tasks);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        var countTasks = await _context.Tasks.CountAsync();
        return countTasks;
    }
}