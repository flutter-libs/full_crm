using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
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
        public async Task<ActionResult<Contact>> CreateContact([FromBody] AddContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var contacts = await _contactRepository.AddContactAsync(contact);
                return Ok(contacts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Contact/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> UpdateContact(int id, [FromBody] UpdateContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var contacts = await _contactRepository.UpdateContactAsync(id, contact);
                return Ok(contacts);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation($"Updating Contact with id {id} failed", ex);
                return BadRequest($"Failed to update Contact with id - DbUpdateConcurrencyException {id}");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation($"Updating Contact with id {id} failed", ex);
                return BadRequest($"Failed to update Contact with id - DbUpdateException {id}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Updating Contact with id {id} failed", ex);
                return BadRequest($"Failed to update Contact with id - Exception {id}");
            }
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

        [HttpGet("notes")]
        public async Task<ActionResult<IEnumerable<ContactNotes>>> GetContactNotes()
        {
            var contactNotes = await _contactNotesRepository.GetAllContactNotesAsync();
            return Ok(contactNotes);
        }

        [HttpGet("notes/{id}")]
        public async Task<ActionResult<ContactNotes>> GetContactNotes(int id)
        {
            var contactNotes = await _contactNotesRepository.GetContactNoteById(id);
            return Ok(contactNotes);
        }

        [HttpPut("notes/{id}")]
        public async Task<ActionResult> UpdateContactNotes(int id, [FromBody] UpdateContactNoteViewModel notes)
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
                _logger.LogInformation($"Updating Contact Note with id {id} failed", ex);
                return BadRequest($"Failed to update Contact Note with id - DbUpdateConcurrencyException {id}");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation($"Updating Contact Note with id {id} failed", ex);
                return BadRequest($"Failed to update Contact Note with id - DbUpdateException {id}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Updating Contact Note with id {id} failed", ex);
                return BadRequest($"Failed to update Contact Note with id - Exception {id}");
            }
        }

        [HttpPost("notes")]
        public async Task<ActionResult<ContactNotes>> CreateContactNotes([FromBody] AddContactNoteViewModel notes)
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

        [HttpDelete("notes/{id}")]
        public async Task<ActionResult> DeleteContactNotes(int id)
        {
            await _contactNotesRepository.DeleteAsync(id);
            return Ok("Notes deleted.");
        }

        [HttpGet("notes/count")]
        public async Task<ActionResult<int>> GetContactNotesCount()
        {
            return Ok(await _contactNotesRepository.CountAsync());
        }
    }