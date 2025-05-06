using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class ContactNotes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
    public int NoteId { get; set; }
    public Note Note { get; set; }
}