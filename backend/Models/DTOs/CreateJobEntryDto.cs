using System.ComponentModel.DataAnnotations;
using backend.Models.Enums;

namespace backend.Models.DTOs;

public record CreateJobEntryDto
{
    [Required]
    [MaxLength(100)]
    public required string CompanyName { get; init; }
    
    [Required]
    [MaxLength(100)]
    public required string JobTitle { get; init; }
    public DateTime? DateApplied { get; init; }
    public ApplicationStatus ApplicationStatus {get; init;}
    

    public string? Notes { get; init; }
    public JobSource? JobSource {get; init;}
    [MaxLength(2000)]
    public string? PostingUrl { get; init; }
    [Range(0.0, 10_000_000.0)]
    public decimal? SalaryMin { get; init; }
    [Range(0.0, 10_000_000.0)]
    public decimal? SalaryMax { get; init; }
    
    [MaxLength(100)]
    public string? RecruiterName { get; init; }
    [MaxLength(254)]
    [EmailAddress]
    public string? RecruiterEmail { get; init; }
    public DateTime? InterviewDate  { get; init; }
}