using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Areas.Main.Models;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Industry { get; set; }
    [MaxLength(100)]
    public string Website { get; set; }
    [MaxLength(200)]
    public string Address { get; set; }
    [MaxLength(50)]
    public string City { get; set; }
    [MaxLength(50)]
    public string State { get; set; }
    [MaxLength(50)]
    public string Country { get; set; }
    [MaxLength(20)]
    public string ZipCode { get; set; }
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [MaxLength(20)]
    public string Fax { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public IEnumerable<Contact>? Contacts { get; set; }
    public IEnumerable<Tasks>? Tasks { get; set; }
    public IEnumerable<CompanyNotes>? CompanyNotes { get; set; }
    public virtual IEnumerable<CompanyTask>? CompanyTasks { get; set; }
}