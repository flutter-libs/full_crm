using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ILeadRepository
{
    Task<IEnumerable<Lead>> GetAllLeadsAsync();
    Task<Lead> GetLeadByIdAsync(int id);
    Task<Lead> AddLeadAsync(Lead lead);
    Task<Lead> UpdateLeadAsync(Lead lead);
    Task<bool> DeleteLeadAsync(int id);
    Task<int> CountLeadsAsync();
}