using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class LeadTaskRepository : ILeadTaskRepository
{
    private readonly ApplicationDbContext _context;

    public LeadTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<LeadTask>> GetAllLeadTasks()
    {
        return await _context.LeadTasks
            .Include(x => x.Tasks)
            .Include(xc => xc.Lead)
            .ToListAsync();
    }

    public async Task<LeadTask?> GetLeadTaskById(int id)
    {
        var leadTask = await _context.LeadTasks
            .Include(x => x.Tasks)
            .Include(x => x.Lead)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (leadTask == null)
        {
            throw new NullReferenceException($"Lead task with id {id} was not found");
        }
        return leadTask;
    }

    public async Task CreateLeadTask(AddLeadTaskViewModel leadTask)
    {
        var leadTasks = new LeadTask
        {
            LeadId = leadTask.LeadId,
            Tasks = new Tasks
            {
                TaskDescription = leadTask.Tasks.TaskDescription,
                Status = leadTask.Tasks.Status,
                Priority = leadTask.Tasks.Priority,
                DueDate = leadTask.Tasks.DueDate,
                AssignedToUser = leadTask.Tasks.AssignedToUser
            },
            Created = leadTask.Created
        };
        _context.LeadTasks.Add(leadTasks);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLeadTask(int id, [FromBody] UpdateLeadTaskViewModel leadTask)
    {
        var leadTasks = await GetLeadTaskById(id);
        leadTasks!.Tasks.TaskDescription = leadTask.Tasks.TaskDescription;
        leadTasks.Tasks.Priority = leadTask.Tasks.Priority;
        leadTasks.Tasks.Status = leadTask.Tasks.Status;
        leadTasks.Tasks.DueDate = leadTask.Tasks.DueDate;
        leadTasks.Tasks.AssignedToUser = leadTask.Tasks.AssignedToUser;
        leadTasks.Updated = leadTask.Updated;
        _context.Update(leadTasks);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLeadTask(int id)
    {
       var leadTask = await GetLeadTaskById(id);
       _context.LeadTasks.Remove(leadTask!);
       await _context.SaveChangesAsync();
    }

    public async Task<int> CountLeadTasks()
    {
        return await _context.LeadTasks.CountAsync();
    }
}