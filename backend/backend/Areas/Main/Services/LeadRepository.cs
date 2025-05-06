using backend.Areas.Main.Models;
using backend.Data;

namespace backend.Areas.Main.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LeadRepository : ILeadRepository
{
    private readonly ApplicationDbContext _context;

    public LeadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lead>> GetAllLeadsAsync()
    {
        return await _context.Leads
            .Include(l => l.CreatedByUser)
            .Include(l => l.Campaigns)
            .Include(l => l.Meetings)
            .ToListAsync();
    }

    public async Task<Lead> GetLeadByIdAsync(int id)
    {
        return (await _context.Leads
            .Include(l => l.CreatedByUser)
            .Include(l => l.Campaigns)
            .Include(l => l.Meetings)
            .FirstOrDefaultAsync(l => l.Id == id))!;
    }

    public async Task<Lead> AddLeadAsync(Lead lead)
    {
        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();
        return lead;
    }

    public async Task<Lead> UpdateLeadAsync(Lead lead)
    {
        _context.Leads.Update(lead);
        await _context.SaveChangesAsync();
        return lead;
    }

    public async Task<bool> DeleteLeadAsync(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        if (lead == null) return false;

        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> CountLeadsAsync()
    {
        return await _context.Leads.CountAsync();
    }
}