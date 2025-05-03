using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Communication.Models;

public class EmailMessage
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(From))]
    public string From { get; set; }
    public User FromUser { get; set; }
    [ForeignKey(nameof(To))]
    public string To { get; set; }
    public User ToUser { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsSent { get; set; } = false;
    public string? ErrorMessage { get; set; }
}