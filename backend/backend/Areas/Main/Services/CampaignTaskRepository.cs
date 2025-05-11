using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CampaignTaskRepository : ICampaignTaskRepository
{
    private readonly ApplicationDbContext _context;

    public CampaignTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CampaignTask>> GetAllCampaignTasks()
    {
        return await _context.CampaignTasks
            .Include(x => x.Campaign)
            .Include(x => x.Tasks)
            .ToListAsync();
    }

    public async Task<CampaignTask> GetCampaignTask(int id)
    {
        var campaignTask = await _context.CampaignTasks
            .Include(x => x.Campaign)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (campaignTask == null)
        {
            throw new NullReferenceException("Campaign task not found");
        }
        return campaignTask;
    }

    public async Task AddCampaignTask([FromBody] AddCampaignTaskViewModel task)
    {
        var campaignTask = new CampaignTask
        {
            Tasks = new Tasks
            {
                TaskDescription = task.Tasks.TaskDescription,
                DueDate = task.Tasks.DueDate,
                Status = task.Tasks.Status,
                Priority = task.Tasks.Priority,
                AssignedToUser = task.Tasks.AssignedToUser,
                DateCreated = task.Tasks.DateCreated,
            },
            CampaignId = task.CampaignId,
            Created = task.Created
        };
        _context.CampaignTasks.Add(campaignTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCampaignTask(int id, UpdateCampaignTaskViewModel task)
    {
        var campaignTask = await GetCampaignTask(id);
        campaignTask.Tasks.TaskDescription = task.Tasks.TaskDescription;
        campaignTask.Tasks.DueDate = task.Tasks.DueDate;
        campaignTask.Tasks.Status = task.Tasks.Status;
        campaignTask.Tasks.Priority = task.Tasks.Priority;
        campaignTask.Tasks.AssignedToUser = task.Tasks.AssignedToUser;
        campaignTask.Tasks.DateUpdated = task.Tasks.DateUpdated;
        campaignTask.CampaignId = task.CampaignId;
        campaignTask.Updated = task.Updated;
        _context.CampaignTasks.Update(campaignTask);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCampaignTask(int id)
    {
        var campaignTask = await GetCampaignTask(id);
        _context.CampaignTasks.Remove(campaignTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountCampaignTasks()
    {
        return await _context.CampaignTasks.CountAsync();
    }
}