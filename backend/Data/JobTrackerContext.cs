using backend.Models.Entities;
using backend.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class JobTrackerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<JobEntry> JobEntries { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public JobTrackerContext(DbContextOptions<JobTrackerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var jobEntry = modelBuilder.Entity<JobEntry>();
        jobEntry.Property(j => j.ApplicationStatus).HasConversion<string>();
        jobEntry.Property(j => j.JobSource).HasConversion<string>();
        jobEntry.Property(j => j.CompanyName).HasMaxLength(100);
        jobEntry.Property(j => j.JobTitle).HasMaxLength(100);
        jobEntry.Property(j => j.RecruiterName).HasMaxLength(100);
        jobEntry.Property(j => j.RecruiterEmail).HasMaxLength(254);
        jobEntry.Property(j => j.PostingUrl).HasMaxLength(2000);
        
        var user = modelBuilder.Entity<User>();
        user.HasIndex(u => u.Email).IsUnique();
        user.Property(u => u.Email).HasMaxLength(254);
        user.Property(u => u.Username).HasMaxLength(50);
    }
}