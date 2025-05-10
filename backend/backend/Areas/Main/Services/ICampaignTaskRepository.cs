using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ICampaignTaskRepository
{
    Task<IEnumerable<CampaignTask>> GetAllCampaignTasks();
    Task<CampaignTask> GetCampaignTask(int id);
    Task AddCampaignTask([FromBody] AddCampaignTaskViewModel task);
    Task UpdateCampaignTask(int id, [FromBody] UpdateCampaignTaskViewModel task);
    Task DeleteCampaignTask(int id);
    Task<int> CountCampaignTasks();
 }