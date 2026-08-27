using backend.Models.Enums;

namespace backend.Models.Entities;

public class JobEntry
{
    public Guid JobEntryId { get; set; }
    public User? User { get; set; }
    public Guid UserId { get; set; }
    public required string CompanyName { get; set; }
    public required string JobTitle { get; set; }
    public DateTime? DateApplied { get; set; }
    public ApplicationStatus ApplicationStatus {get; set;}
    public string? Notes { get; set; }
    public JobSource? JobSource {get; set;}
    public string? PostingUrl { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? RecruiterName { get; set; }
    public string? RecruiterEmail { get; set; }
    public DateTime? InterviewDate  { get; set; }
}