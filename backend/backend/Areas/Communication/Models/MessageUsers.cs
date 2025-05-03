using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Communication.Models;

public class MessageUsers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int MessageId { get; set; }
    [ForeignKey("MessageId")]
    public Message? Message { get; set; }
    public string FromId { get; set; }
    [ForeignKey("FromId")]
    public User From { get; set; }
    public virtual IEnumerable<User> Receivers { get; set; }
}