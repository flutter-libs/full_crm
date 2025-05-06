using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class JobNotes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public int NoteId { get; set; }
    public Note Note { get; set; }
}