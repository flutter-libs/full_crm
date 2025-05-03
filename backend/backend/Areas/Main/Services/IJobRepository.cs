using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllJobsAsync();
    Task<Job?> GetJobByIdAsync(int id);
    Task<Job> CreateJobAsync(Job job);
    Task<Job?> UpdateJobAsync(int id, Job updatedJob);
    Task<bool> DeleteJobAsync(int id);
    Task<int> CountAllJobsAsync();
}