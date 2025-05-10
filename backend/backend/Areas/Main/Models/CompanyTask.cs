using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class CompanyTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int TaskId { get; set; }
    public Tasks Tasks { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}