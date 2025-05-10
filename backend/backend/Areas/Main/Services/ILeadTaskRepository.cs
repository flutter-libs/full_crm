using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ILeadTaskRepository
{
    Task<IEnumerable<LeadTask>> GetAllLeadTasks();
    Task<LeadTask?> GetLeadTaskById(int id);
    Task CreateLeadTask([FromBody] AddLeadTaskViewModel leadTask);
    Task UpdateLeadTask(int id, [FromBody] UpdateLeadTaskViewModel leadTask);
    Task DeleteLeadTask(int id);
    Task<int> CountLeadTasks();
}