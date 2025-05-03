using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task<int> GetContactsCountAsync();
    Task DeleteContactAsync(int id);
    Task<IEnumerable<Contact>> GetContactsByOwnerAsync(string ownerUserId);
}