using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class CampaignTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Campaign Campaign { get; set; }
    public int CampaignId { get; set; }
    public Tasks Tasks  { get; set; }
    public int TaskId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}