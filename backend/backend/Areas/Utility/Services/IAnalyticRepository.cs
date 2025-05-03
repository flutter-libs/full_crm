using backend.Areas.Utility.Models;

namespace backend.Areas.Utility.Services;

public interface IAnalyticRepository
{
    Task<IEnumerable<Analytic>> GetAllAsync();
    Task<Analytic> GetByIdAsync(int id);
    Task<Analytic> AddAsync(Analytic analytic);
    Task UpdateAsync(Analytic analytic);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}