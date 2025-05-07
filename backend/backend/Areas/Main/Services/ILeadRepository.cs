using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ILeadRepository
{
    Task<IEnumerable<Lead>> GetAllLeadsAsync();
    Task<Lead> GetLeadByIdAsync(int id);
    Task<Lead> AddLeadAsync([FromBody] AddLeadViewModel lead);
    Task<Lead> UpdateLeadAsync(int id, [FromBody] UpdateLeadViewModel lead);
    Task<bool> DeleteLeadAsync(int id);
    Task<int> CountLeadsAsync();
}