using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;
using backend.Areas.Utility.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.Areas.Utility.Models;

public class Analytic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Category Category { get; set; }
    public string MetricName { get; set; }
    public string Description { get; set; }
    [Precision(18,2)]
    public decimal Value { get; set; }
    public Period Period { get; set; }
    public DateTime RecordedDate { get; set; } = DateTime.Now;
    public string? CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedByUser { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; }
}