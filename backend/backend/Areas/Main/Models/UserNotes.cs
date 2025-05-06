using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Main.Models;

public class UserNotes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int NoteId { get; set; }
    public Note Note { get; set; }
}