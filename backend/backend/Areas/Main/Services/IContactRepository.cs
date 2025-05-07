using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task<Contact> AddContactAsync([FromBody] AddContactViewModel contact);
    Task<Contact> UpdateContactAsync(int id, [FromBody] UpdateContactViewModel contact);
    Task<int> GetContactsCountAsync();
    Task DeleteContactAsync(int id);
    Task<IEnumerable<Contact>> GetContactsByOwnerAsync(string ownerUserId);
}