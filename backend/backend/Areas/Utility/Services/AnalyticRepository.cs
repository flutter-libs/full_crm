using backend.Areas.Utility.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Utility.Services;

public class AnalyticRepository : IAnalyticRepository
{
    private readonly ApplicationDbContext _context;

    public AnalyticRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Analytic>> GetAllAsync()
    {
        return await _context.Analytics
            .Include(a => a.CreatedByUser)
            .ToListAsync();
    }

    public async Task<Analytic> GetByIdAsync(int id)
    {
        return (await _context.Analytics
            .Include(a => a.CreatedByUser)
            .FirstOrDefaultAsync(a => a.Id == id))!;
    }

    public async Task<Analytic> AddAsync(Analytic analytic)
    {
        _context.Analytics.Add(analytic);
        await _context.SaveChangesAsync();
        return analytic;
    }

    public async Task UpdateAsync(Analytic analytic)
    {
        _context.Analytics.Update(analytic);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var analytic = await _context.Analytics.FindAsync(id);
        if (analytic != null)
        {
            _context.Analytics.Remove(analytic);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Analytics.AnyAsync(a => a.Id == id);
    }
}