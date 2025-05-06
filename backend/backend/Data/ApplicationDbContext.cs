using backend.Areas.Communication.Models;
using backend.Areas.Identity.Models;
using backend.Areas.Main.Models;
using backend.Areas.Utility.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User, Role, string,
    IdentityUserClaim<string>, UserRoles, IdentityUserLogin<string>, IdentityRoleClaim<string>,
    IdentityUserToken<string>>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Lead> Leads { get; set; }
    public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<EmailMessage> Emails { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Tasks?> Tasks { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Analytic> Analytics { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageUsers> MessageUsers { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<UserMeeting> UserMeetings { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<UserNotes> UserNotes { get; set; }
    public DbSet<TaskNotes> TaskNotes { get; set; }
    public DbSet<LeadNotes> LeadNotes { get; set; }
    public DbSet<ContactNotes> ContactNotes { get; set; }
    public DbSet<CampaignNotes> CampaignNotes { get; set; }
    public DbSet<CompanyNotes> CompanyNotes { get; set; }
    public DbSet<JobNotes> JobNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(x => x.Contacts)
                .WithOne(x => x.OwnerUser)
                .HasForeignKey(x => x.OwnerUserId);
            entity.HasMany(x => x.Leads)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedBy);
            entity.HasMany(x => x.AssignedJobs)
                .WithOne(x => x.AssignedUser)
                .HasForeignKey(x => x.AssignedUserId);
            entity.HasMany(x => x.CreatedJobs)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId);
            entity.HasMany(x => x.Tasks)
                .WithOne(c => c.AssignedToUser)
                .HasForeignKey(c => c.AssignedToUserId)
                .HasPrincipalKey(u => u.Id);
            entity.HasMany(x => x.Campaigns)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasPrincipalKey(u => u.Id);
            entity.HasMany(x => x.Analytics)
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasPrincipalKey(u => u.Id);
            entity.HasMany(x => x.MessageUsers)
                .WithOne(u => u.From)
                .HasForeignKey(x => x.FromId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(x => x.UserMeetings)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        });
        builder.Entity<Role>(entity => { entity.HasKey(e => e.Id); });
        builder.Entity<UserRoles>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId, e.RoleId });
            entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
        });
        builder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedBy });
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.CreatedBy);
            entity.HasMany(x => x.Meetings)
                .WithOne(x => x.Lead)
                .HasForeignKey(x => x.LeadId)
                .HasPrincipalKey(l => l.Id);
        });
        builder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OwnerUserId });
            entity.HasOne(x => x.OwnerUser)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.OwnerUserId);
            entity.HasMany(x => x.Tasks)
                .WithOne(c => c.Contact)
                .HasForeignKey(c => c.ContactId)
                .HasPrincipalKey(c => c.Id);
            entity.HasOne(x => x.Company)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(x => x.Meetings)
                .WithOne(x => x.Contact)
                .HasForeignKey(x => x.ContactId)
                .HasPrincipalKey(c => c.Id);
        });
        builder.Entity<Job>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.AssignedUserId, e.CreatedByUserId, e.ContactId });
            entity.HasOne(x => x.AssignedUser)
                .WithMany(x => x.AssignedJobs)
                .HasForeignKey(x => x.AssignedUserId);
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.CreatedJobs)
                .HasForeignKey(x => x.CreatedByUserId);
            entity.HasOne(x => x.Contact)
                .WithMany(x => x.Jobs)
                .HasForeignKey(x => x.ContactId)
                .HasPrincipalKey(x => x.Id);
            entity.HasMany(x => x.Tasks)
                .WithOne(c => c.Job)
                .HasForeignKey(c => c.JobId)
                .HasPrincipalKey(x => x.Id);
        });
        builder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedByUserId });
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(x => x.Campaigns)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(c => c.Leads)
                .WithMany(x => x.Campaigns)
                .UsingEntity(j => j.ToTable("CampaignLeads"));
            entity.HasMany(x => x.Contacts)
                .WithMany(c => c.Campaigns)
                .UsingEntity(j => j.ToTable("CampaignContacts"));
            entity.HasMany(x => x.Tasks)
                .WithOne(x => x.Campaign)
                .HasForeignKey(x => x.CampaignId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.To, e.From });
            entity.HasOne(x => x.FromUser)
                .WithMany()
                .HasForeignKey(e => e.From)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.ToUser)
                .WithMany()
                .HasForeignKey(e => e.To)
                .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<Tasks>(entity =>
        {
            entity.HasKey(c => new { c.Id, c.ContactId, c.CampaignId, c.JobId, c.AssignedToUserId, c.CompanyId });
            entity.HasOne(c => c.AssignedToUser)
                .WithMany(t => t.Tasks)
                .HasForeignKey(c => c.AssignedToUserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c => c.Contact)
                .WithMany(c => c.Tasks)
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c => c.Job)
                .WithMany(c => c.Tasks)
                .HasForeignKey(c => c.JobId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(c => c.Company)
                .WithMany(t => t.Tasks)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<Company>(entity => { entity.HasKey(e => e.Id); });
        builder.Entity<Analytic>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CreatedByUserId });
            entity.HasOne(x => x.CreatedByUser)
                .WithMany(c => c.Analytics)
                .HasForeignKey(x => x.CreatedByUserId)
                .HasPrincipalKey(x => x.Id);
        });
        builder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(m => m.MessageUsers)
                .WithOne(m => m.Message)
                .HasForeignKey(m => m.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<MessageUsers>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.MessageId, e.FromId });
            entity.HasOne(mu => mu.From)
                .WithMany(u => u.MessageUsers)
                .HasForeignKey(mu => mu.FromId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(mu => mu.Receivers)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "MessageUserReceiver",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<MessageUsers>()
                        .WithMany()
                        .HasForeignKey("MessageUserId")
                        .HasPrincipalKey(m => m.Id)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("MessageUserId", "ReceiverId");
                        j.ToTable("MessageUserReceivers");
                    });
        });
        builder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OrganizerId });
            entity.HasMany(x => x.UserMeetings)
                .WithOne(m => m.Meeting)
                .HasForeignKey(m => m.MeetingId)
                .HasPrincipalKey(m => m.Id);
            entity.HasOne(x => x.Organizer)
                .WithMany(c => c.Meetings)
                .HasForeignKey(j => j.OrganizerId);
        });
        builder.Entity<UserMeeting>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId, e.MeetingId });
            entity.HasOne(mu => mu.Meeting)
                .WithMany(c => c.UserMeetings)
                .HasForeignKey(x => x.MeetingId);
            entity.HasOne(mu => mu.User)
                .WithMany(c => c.UserMeetings)
                .HasForeignKey(j => j.UserId);
        });
        builder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<UserNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.NoteId, e.UserId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.UserNotes)
                .HasForeignKey(n => n.NoteId);
            entity.HasOne(n => n.User)
                .WithMany(n => n.UserNotes)
                .HasForeignKey(n => n.UserId);
        });
        builder.Entity<TaskNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.TaskId, e.NoteId });
            entity.HasOne(t => t.Note)
                .WithMany(n => n.TaskNotes)
                .HasForeignKey(t => t.NoteId);
            entity.HasOne(t => t.Task)
                .WithMany(n => n.TaskNotes)
                .HasForeignKey(t => t.TaskId);
        });
        builder.Entity<LeadNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.LeadId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.LeadNotes)
                .HasForeignKey(n => n.NoteId);
            entity.HasOne(n => n.Lead)
                .WithMany(n => n.LeadNotes)
                .HasForeignKey(n => n.LeadId);
        });
        builder.Entity<ContactNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ContactId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.ContactNotes)
                .HasForeignKey(n => n.NoteId);
            entity.HasOne(n => n.Contact)
                .WithMany(n => n.ContactNotes)
                .HasForeignKey(n => n.ContactId);
        });
        builder.Entity<CompanyNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CompanyId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.CompanyNotes)
                .HasForeignKey(n => n.NoteId);
            entity.HasOne(n => n.Company)
                .WithMany(n => n.CompanyNotes)
                .HasForeignKey(n => n.CompanyId);
        });
        builder.Entity<CampaignNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CampaignId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.CampaignNotes)
                .HasForeignKey(n => n.NoteId);
            entity.HasOne(n => n.Campaign)
                .WithMany(n => n.CampaignNotes)
                .HasForeignKey(n => n.CampaignId);
        });
        builder.Entity<JobNotes>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.JobId, e.NoteId });
            entity.HasOne(n => n.Note)
                .WithMany(n => n.JobNotes)
                .HasForeignKey(n => n.NoteId);
            entity.HasOne(n => n.Job)
                .WithMany(n => n.JobNotes)
                .HasForeignKey(n => n.JobId);
        });
    }
}