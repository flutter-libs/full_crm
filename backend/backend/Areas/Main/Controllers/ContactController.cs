using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

    
    [ApiController]
    [Area("Main")]
    [Route("api/[area]/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactController> _logger;
        private readonly IContactNotesRepository _contactNotesRepository;

        public ContactController(ApplicationDbContext context, IContactRepository contactRepository, ILogger<ContactController> logger,
            IContactNotesRepository contactNotesRepository)
        {
            _context = context;
            _contactRepository = contactRepository;
            _logger = logger;
            _contactNotesRepository = contactNotesRepository;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return Ok(contacts);
        }

        // GET: api/Contact/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetContactById(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // GET: api/Contact/owner/{ownerUserId}
        [HttpGet("owner/{ownerUserId}")]
        public async Task<ActionResult> GetContactsByOwner(string ownerUserId)
        {
            var contacts = await _contactRepository.GetContactsByOwnerAsync(ownerUserId);
            return Ok(contacts);
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.AddContactAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        // PUT: api/Contact/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContact(int id, [FromBody] Contact contact)
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
        public async Task<ActionResult> DeleteContact(int id)
        {
            var existingContact = await _contactRepository.GetContactByIdAsync(id);
            if (existingContact == null)
                return NotFound();

            await _contactRepository.DeleteContactAsync(id);
            return NoContent();
        }

        [HttpGet("contactCount")]
        public async Task<ActionResult<int>> GetContactCount()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("contactNotes")]
        public async Task<ActionResult<IEnumerable<ContactNotes>>> GetContactNotes()
        {
            var contactNotes = await _contactNotesRepository.GetAllContactNotesAsync();
            return Ok(contactNotes);
        }

        [HttpGet("contactNotes/{id}")]
        public async Task<ActionResult<ContactNotes>> GetContactNotes(int id)
        {
            var contactNotes = await _contactNotesRepository.GetContactNoteById(id);
            return Ok(contactNotes);
        }

        [HttpPut("contactNotes/{id}")]
        public async Task<ActionResult> UpdateContactNotes(int id, [FromBody] ContactNotes notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _contactNotesRepository.UpdateAsync(id, notes);
                return Ok("Notes updated.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
                return BadRequest($"Failed to update Campaign Note with id - DbUpdateConcurrencyException {id}");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
                return BadRequest($"Failed to update Campaign Note with id - DbUpdateException {id}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
                return BadRequest($"Failed to update Campaign Note with id - Exception {id}");
            }
        }

        [HttpPost("contactNotes")]
        public async Task<ActionResult<ContactNotes>> CreateContactNotes([FromBody] ContactNotes notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _contactNotesRepository.AddAsync(notes);
                return Ok("Notes created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("contactNotes/{id}")]
        public async Task<ActionResult> DeleteContactNotes(int id)
        {
            await _contactNotesRepository.DeleteAsync(id);
            return Ok("Notes deleted.");
        }

        [HttpGet("contactNotes/count")]
        public async Task<ActionResult<int>> GetContactNotesCount()
        {
            return Ok(await _contactNotesRepository.CountAsync());
        }
    }