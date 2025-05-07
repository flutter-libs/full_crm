using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
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
            .Include(n => n.ContactNotes)!
            .ThenInclude(n => n.Note)
            .Include(c => c.Tasks)!
            .ThenInclude(ct => ct.TaskNotes)!
            .ThenInclude(ct => ct.Note)
            .ToListAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        var contact = await _context.Contacts
            .Include(c => c.OwnerUser)
            .Include(n => n.ContactNotes)!
            .ThenInclude(n => n.Note)
            .Include(c => c.Tasks)!
            .ThenInclude(ct => ct.TaskNotes)!
            .ThenInclude(ct => ct.Note)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (contact == null)
        {
            throw new NullReferenceException("Contact not found");
        }
        return contact;
    }

    public async Task<Contact> AddContactAsync([FromBody] AddContactViewModel contact)
    {
        var contacts = new Contact
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            JobTitle = contact.JobTitle,
            CompanyName = contact.CompanyName,
            PhoneNumber = contact.PhoneNumber,
            AddressLine1 = contact.AddressLine1,
            AddressLine2 = contact.AddressLine2,
            City = contact.City,
            State = contact.State,
            ZipCode = contact.ZipCode,
            Country = contact.Country,
            Notes = contact.Notes,
            DateCreated = contact.DateCreated,
            OwnerUserId = contact.OwnerUserId,
            ImageUrl = contact.ImageUrl,
        };
        _context.Contacts.Add(contacts);
        await _context.SaveChangesAsync();
        return contacts;
    }

    public async Task<Contact> UpdateContactAsync(int id, [FromBody] UpdateContactViewModel contact)
    {
        var contactToUpdate = await GetContactByIdAsync(id);
        contactToUpdate.FirstName = contact.FirstName;
        contactToUpdate.LastName = contact.LastName;
        contactToUpdate.JobTitle = contact.JobTitle;
        contactToUpdate.AddressLine1 = contact.AddressLine1;
        contactToUpdate.AddressLine2 = contact.AddressLine2;
        contactToUpdate.City = contact.City;
        contactToUpdate.State = contact.State;
        contactToUpdate.ZipCode = contact.ZipCode;
        contactToUpdate.Country = contact.Country;
        contactToUpdate.PhoneNumber = contact.PhoneNumber;
        contactToUpdate.Email = contact.Email;
        contactToUpdate.CompanyName = contact.CompanyName;
        contactToUpdate.DateUpdated = contact.DateUpdated;
        contactToUpdate.ImageUrl = contact.ImageUrl;
        contactToUpdate.Notes = contact.Notes;
        _context.Contacts.Update(contactToUpdate);
        await _context.SaveChangesAsync();
        await _context.SaveChangesAsync();
        return contactToUpdate;
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