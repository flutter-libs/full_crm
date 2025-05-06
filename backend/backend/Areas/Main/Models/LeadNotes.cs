using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class LeadNotes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int LeadId { get; set; }
    public Lead Lead { get; set; }
    public int NoteId { get; set; }
    public Note Note { get; set; }
}