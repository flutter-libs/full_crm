using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CampaignRepository : ICampaignRepository
{
    private readonly ApplicationDbContext _context;

    public CampaignRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Campaign>> GetAllAsync()
    {
        return await _context.Campaigns
            .Include(c => c.CreatedByUser)
            .Include(c => c.Leads)
            .Include(c => c.Contacts)
            .Include(c => c.Tasks)
            .ToListAsync();
    }

    public async Task<Campaign> GetByIdAsync(int id)
    {
        return (await _context.Campaigns
            .Include(c => c.CreatedByUser)
            .Include(c => c.Leads)
            .Include(c => c.Contacts)
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(c => c.Id == id))!;
    }

    public async Task<Campaign> AddAsync(Campaign campaign)
    {
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
        return campaign;
    }

    public async Task UpdateAsync(Campaign campaign)
    {
        _context.Campaigns.Update(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign != null)
        {
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Campaigns.AnyAsync(c => c.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Campaigns.CountAsync();
    }
}