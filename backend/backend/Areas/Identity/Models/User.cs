using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Communication.Models;
using backend.Areas.Main.Models;
using backend.Areas.Utility.Models;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Areas.Identity.Models;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Description { get; set; }
    public int? MessageUserId { get; set; }
    [NotMapped]
    [ForeignKey(nameof(MessageUserId))]
    public MessageUsers? MessageUser { get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    

    public DateTime DateCreated { get; set; }
    public IEnumerable<Campaign>? Campaigns { get; set; }
    public IEnumerable<UserRoles>? UserRoles { get; set; }
    public IEnumerable<Lead>? Leads { get; set; }
    public IEnumerable<Contact>? Contacts { get; set; }
    public IEnumerable<Job>? CreatedJobs { get; set; }
    public IEnumerable<Job>? AssignedJobs { get; set; }
    public IEnumerable<Analytic>? Analytics { get; set; }
    public IEnumerable<Tasks>? Tasks { get; set; }
    public IEnumerable<MessageUsers>? MessageUsers { get; set; }
}