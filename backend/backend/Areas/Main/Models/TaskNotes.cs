using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class TaskNotes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TaskId { get; set; }
    public Tasks Task { get; set; }
    public int NoteId { get; set; }
    public Note Note { get; set; }
}