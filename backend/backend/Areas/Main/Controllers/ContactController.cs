using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Controllers;

    
    [ApiController]
    [Area("Main")]
    [Route("api/[area]/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ApplicationDbContext context, IContactRepository contactRepository, ILogger<ContactController> logger)
        {
            _context = context;
            _contactRepository = contactRepository;
            _logger = logger;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return Ok(contacts);
        }

        // GET: api/Contact/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // GET: api/Contact/owner/{ownerUserId}
        [HttpGet("owner/{ownerUserId}")]
        public async Task<IActionResult> GetContactsByOwner(string ownerUserId)
        {
            var contacts = await _contactRepository.GetContactsByOwnerAsync(ownerUserId);
            return Ok(contacts);
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.AddContactAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        // PUT: api/Contact/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (id != contact.Id)
                return BadRequest("Contact ID mismatch.");

            var existingContact = await _contactRepository.GetContactByIdAsync(id);
            if (existingContact == null)
                return NotFound();

            await _contactRepository.UpdateContactAsync(contact);
            return NoContent();
        }

        // DELETE: api/Contact/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var existingContact = await _contactRepository.GetContactByIdAsync(id);
            if (existingContact == null)
                return NotFound();

            await _contactRepository.DeleteContactAsync(id);
            return NoContent();
        }
    }