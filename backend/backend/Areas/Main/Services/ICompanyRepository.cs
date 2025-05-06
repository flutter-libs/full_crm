using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompaniesAsync();
    Task<Company> GetCompanyById(int id);
    Task<Company> AddAsync(Company company);
    Task UpdateAsync(int id, Company company);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}