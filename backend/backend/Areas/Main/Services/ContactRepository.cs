using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _context.Contacts
            .Include(c => c.OwnerUser)
            .ToListAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        return await _context.Contacts
            .Include(c => c.OwnerUser)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddContactAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetContactsCountAsync()
    {
        return await _context.Contacts.CountAsync();
    }

    public async Task DeleteContactAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Contact>> GetContactsByOwnerAsync(string ownerUserId)
    {
        return await _context.Contacts
            .Where(c => c.OwnerUserId == ownerUserId)
            .Include(c => c.OwnerUser)
            .ToListAsync();
    }
}