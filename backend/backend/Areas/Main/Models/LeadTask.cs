using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class LeadTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int LeadId { get; set; }
    public Lead Lead { get; set; }
    public int TaskId { get; set; }
    public Tasks Tasks { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}