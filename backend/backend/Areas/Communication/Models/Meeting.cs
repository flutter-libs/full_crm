using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using backend.Areas.Main.Models;

namespace backend.Areas.Communication.Models;

public class Meeting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(200)]
    public string Title { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }

    public bool IsOnline { get; set; } = true;

    [StringLength(500)]
    public string? Location { get; set; }

    [StringLength(500)]
    public string? MeetingLink { get; set; }
    
    public string OrganizerId { get; set; }

    [ForeignKey(nameof(OrganizerId))]
    public User Organizer { get; set; }

    // Participants - many-to-many relationship with User
    public ICollection<UserMeeting> UserMeetings { get; set; }

    // Related Lead (optional)
    public int? LeadId { get; set; }

    [ForeignKey(nameof(LeadId))]
    public Lead? Lead { get; set; }

    // Related Contact (optional)
    public int? ContactId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public Contact? Contact { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}