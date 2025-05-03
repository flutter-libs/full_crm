using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Communication.Models;

public class UserMeeting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public int MeetingId { get; set; }
    [ForeignKey(nameof(MeetingId))]
    public Meeting Meeting { get; set; }
}