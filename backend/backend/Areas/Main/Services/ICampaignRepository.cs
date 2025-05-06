using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ICampaignRepository
{
    Task<IEnumerable<Campaign>> GetAllAsync();
    Task<Campaign> GetByIdAsync(int id);
    Task<Campaign> AddAsync(Campaign campaign);
    Task UpdateAsync(Campaign campaign);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> CountAsync();
}