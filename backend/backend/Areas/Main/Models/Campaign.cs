using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Models;

public class Campaign
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(150)]
    public string Name { get; set; }

    [StringLength(300)]
    public string Description { get; set; }

    [StringLength(100)]
    public string Type { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    [Precision(18,2)]
    public decimal? Budget { get; set; }
    
    [Precision(18,2)]
    public decimal? ActualCost { get; set; }

    public int? ExpectedResponses { get; set; }
    public int? ActualResponses { get; set; }

    public decimal? ExpectedSales { get; set; }
    public decimal? ActualSales { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; set; }

    // Optional: Foreign key to the user who created the campaign
    public string CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedByUser { get; set; }

    // Optional: Collection of leads or contacts associated with this campaign
    public virtual ICollection<Lead> Leads { get; set; }
    public virtual ICollection<Contact> Contacts { get; set; }
    public virtual ICollection<Tasks> Tasks { get; set; }
    public virtual IEnumerable<CampaignNotes> CampaignNotes { get; set; }
    public virtual IEnumerable<CampaignTask> CampaignTask { get; set; }
}