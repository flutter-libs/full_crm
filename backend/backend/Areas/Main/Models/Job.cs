using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using backend.Areas.Main.Models.Enums;

namespace backend.Areas.Main.Models;

public class Job
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public Status Status { get; set; }

    [Required]
    [StringLength(50)]
    public Priority Priority { get; set; }

    public DateTime ScheduledDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal EstimatedCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ActualCost { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; set; }
    public int ContactId { get; set; }
    [ForeignKey(nameof(ContactId))]
    public virtual Contact Contact { get; set; }
    public string? AssignedUserId { get; set; }
    [ForeignKey(nameof(AssignedUserId))]
    public virtual User AssignedUser { get; set; }
    public string? CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]
    public User CreatedByUser { get; set; }
    public IEnumerable<Tasks>? Tasks { get; set; }
    public IEnumerable<JobNotes>? JobNotes { get; set; }
    public IEnumerable<JobTask>? JobTasks { get; set; }
}