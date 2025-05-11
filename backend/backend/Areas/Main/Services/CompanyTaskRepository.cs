using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CompanyTaskRepository : ICompanyTaskRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CompanyTask>> GetAllAsync()
    {
        return await _context.CompanyTasks
            .Include(x => x.Company)
            .Include(x => x.Tasks)
            .ToListAsync();
    }

    public async Task<CompanyTask> GetByIdAsync(int id)
    {
        var companyTask = await _context.CompanyTasks
            .Include(x => x.Company)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (companyTask == null)
        {
            throw new NullReferenceException("CompanyTask not found");
        }
        return companyTask;
    }

    public async Task<CompanyTask> AddAsync([FromBody] AddCompanyTaskViewModel companyTask)
    {
        var companyTasks = new CompanyTask
        {
            Tasks = new Tasks
            {
                TaskDescription = companyTask.Tasks.TaskDescription,
                DueDate = companyTask.Tasks.DueDate,
                Status = companyTask.Tasks.Status,
                Priority = companyTask.Tasks.Priority,
                DateCreated = companyTask.Tasks.DateCreated,
                AssignedToUser = companyTask.Tasks.AssignedToUser,
            },
            CompanyId = companyTask.CompanyId,
            Created = companyTask.Created,
        };
        _context.CompanyTasks.Add(companyTasks);
        await _context.SaveChangesAsync();
        return companyTasks;
    }

    public async Task<CompanyTask> UpdateAsync(int id, [FromBody] UpdateCompanyTaskViewModel companyTask)
    {
        var companyTasks = await GetByIdAsync(id);
        companyTasks.Tasks.DueDate = companyTask.Tasks.DueDate;
        companyTasks.Tasks.TaskDescription = companyTask.Tasks.TaskDescription;
        companyTasks.Tasks.Priority = companyTask.Tasks.Priority;
        companyTasks.Tasks.DateUpdated = companyTask.Tasks.DateUpdated;
        companyTasks.Tasks.AssignedToUser = companyTask.Tasks.AssignedToUser;
        companyTasks.Tasks.Status = companyTask.Tasks.Status;
        companyTasks.CompanyId = companyTask.CompanyId;
        companyTasks.Updated = companyTask.Updated;
        _context.CompanyTasks.Update(companyTasks);
        await _context.SaveChangesAsync();
        return companyTasks;
    }

    public async Task DeleteAsync(int id)
    {
        var companyTask = await GetByIdAsync(id);
        _context.CompanyTasks.Remove(companyTask);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.CompanyTasks.CountAsync();
    }
}