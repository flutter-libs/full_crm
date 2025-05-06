using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Main.Models;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public IEnumerable<UserNotes>? UserNotes { get; set; }
    public IEnumerable<LeadNotes>? LeadNotes { get; set; }
    public IEnumerable<TaskNotes>? TaskNotes { get; set; }
    public IEnumerable<CompanyNotes>? CompanyNotes { get; set; }
    public IEnumerable<ContactNotes>? ContactNotes { get; set; }
    public IEnumerable<CampaignNotes>? CampaignNotes { get; set; }
    public IEnumerable<JobNotes>? JobNotes { get; set; }
}