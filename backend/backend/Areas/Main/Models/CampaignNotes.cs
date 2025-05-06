using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class CampaignNotes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    public int NoteId { get; set; }
    public Note Note { get; set; }
}