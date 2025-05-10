using System.ComponentModel.DataAnnotations.Schema;
using backend.Areas.Identity.Models;

namespace backend.Areas.Main.Models.ViewModels;

public class AddLeadViewModel
{
    public string LeadName { get; set; }
    public string? LeadAddress { get; set; }
    public string? LeadCity { get; set; }
    public string? LeadState { get; set; }
    public string? LeadZip { get; set; }
    public string? LeadCountry { get; set; }
    public string LeadPhone { get; set; }
    public string? LeadEmail { get; set; }
    public string? LeadFax { get; set; }
    public string? LeadWebsite { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
}

public class UpdateLeadViewModel
{
    public string LeadName { get; set; }
    public string? LeadAddress { get; set; }
    public string? LeadCity { get; set; }
    public string? LeadState { get; set; }
    public string? LeadZip { get; set; }
    public string? LeadCountry { get; set; }
    public string LeadPhone { get; set; }
    public string? LeadEmail { get; set; }
    public string? LeadFax { get; set; }
    public string? LeadWebsite { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
}

public class AddLeadNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public int LeadId { get; set; }
}

public class UpdateLeadNoteViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}

public class AddLeadTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int LeadId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateLeadTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int LeadId { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}