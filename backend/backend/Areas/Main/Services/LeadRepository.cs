using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LeadRepository : ILeadRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Lead> _leads;
    public LeadRepository(ApplicationDbContext context)
    {
        _context = context;
        _leads = context.Set<Lead>();
    }

    public async Task<IEnumerable<Lead>> GetAllLeadsAsync()
    {
        return await _leads.Include(x => x.CreatedByUser).ThenInclude(u => u.Leads).ToListAsync();
    }

    public async Task<Lead> GetLeadByIdAsync(int id)
    {
        var lead = await _leads
            .Include(l => l.CreatedByUser)
            .FirstOrDefaultAsync(l => l.Id == id);
        if (lead == null)
        {
            throw new NullReferenceException($"Lead with id {id} was not found");
        }

        return lead;
    }

    public async Task<Lead> AddLeadAsync([FromBody] AddLeadViewModel addLead)
    {
        var lead = new Lead
        {
            LeadName = addLead.LeadName,
            LeadEmail = addLead.LeadEmail,
            LeadAddress = addLead.LeadAddress,
            LeadCity = addLead.LeadCity,
            LeadState = addLead.LeadState,
            LeadZip = addLead.LeadZip,
            LeadCountry = addLead.LeadCountry,
            LeadFax = addLead.LeadFax,
            LeadPhone = addLead.LeadPhone,
            LeadWebsite = addLead.LeadWebsite,
            CreatedBy = addLead.CreatedBy,
            Created = addLead.Created
        };
        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();
        return lead;
    }

    public async Task<Lead> UpdateLeadAsync(int id, [FromBody] UpdateLeadViewModel lead)
    {
        var leadToUpdate = await GetLeadByIdAsync(id);
        leadToUpdate.LeadName = lead.LeadName;
        leadToUpdate.LeadEmail = lead.LeadEmail;
        leadToUpdate.LeadAddress = lead.LeadAddress;
        leadToUpdate.LeadCity = lead.LeadCity;
        leadToUpdate.LeadState = lead.LeadState;
        leadToUpdate.LeadZip = lead.LeadZip;
        leadToUpdate.LeadCountry = lead.LeadCountry;
        leadToUpdate.LeadFax = lead.LeadFax;
        leadToUpdate.LeadPhone = lead.LeadPhone;
        leadToUpdate.LeadWebsite = lead.LeadWebsite;
        leadToUpdate.CreatedBy = lead.CreatedBy;
        leadToUpdate.Updated = lead.Updated;
        _leads.Update(leadToUpdate);
        await _context.SaveChangesAsync();
        return leadToUpdate;
    }

    public async Task<bool> DeleteLeadAsync(int id)
    {
        var lead = await GetLeadByIdAsync(id);
        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> CountLeadsAsync()
    {
        return await _context.Leads.CountAsync();
    }
}