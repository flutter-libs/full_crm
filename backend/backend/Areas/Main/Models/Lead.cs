using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Communication.Models;
using backend.Areas.Identity.Models;

namespace backend.Areas.Main.Models;

public class Lead
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string LeadName { get; set; }
    public string? LeadAddress { get; set; }
    public string? LeadCity { get; set; }
    public string? LeadState { get; set; }
    public string? LeadZip { get; set; }
    public string? LeadCountry { get; set; }
    public string LeadPhone { get; set; }
    public string? LeadEmail { get; set; }
    public string? LeadFax { get; set; }
    public string? LeadWebsite { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    
    public string? CreatedBy { get; set; }
    [ForeignKey("CreatedBy")] 
    public User? CreatedByUser { get; set; }
    public virtual IEnumerable<LeadNotes>? LeadNotes{ get; set; }
    public virtual IEnumerable<Campaign>? Campaigns { get; set; }
    public virtual IEnumerable<Meeting>? Meetings { get; set; }
    public virtual IEnumerable<LeadTask>? LeadTasks { get; set; }
}