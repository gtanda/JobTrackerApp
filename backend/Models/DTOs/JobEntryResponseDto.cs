using backend.Models.Enums;

namespace backend.Models.DTOs;

public record JobEntryResponseDto
{
    public Guid JobEntryId { get; init; }
    
    public required string CompanyName { get; init; }
    

    public required string JobTitle { get; init; }
    public DateTime? DateApplied { get; init; }
    public ApplicationStatus ApplicationStatus {get; init;}
    

    public string? Notes { get; init; }
    public JobSource? JobSource {get; init;}

    public string? PostingUrl { get; init; }

    public decimal? SalaryMin { get; init; }

    public decimal? SalaryMax { get; init; }
    

    public string? RecruiterName { get; init; }

    public string? RecruiterEmail { get; init; }
    public DateTime? InterviewDate  { get; init; }
}