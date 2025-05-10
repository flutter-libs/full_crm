using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IJobTaskRepository
{
    Task<IEnumerable<JobTask>> GetJobTasks();
    Task<JobTask> GetJobTask(int id);
    Task<JobTask> CreateJobTask([FromBody] AddJobTaskViewModel jobTask);
    Task<JobTask> UpdateJobTask(int id, [FromBody] UpdateJobTaskViewModel jobTask);
    Task DeleteJobTask(int id);
    Task<int> CountJobTasks();
}