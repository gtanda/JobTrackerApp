using System.Security.Claims;
using backend.Data;
using backend.Models.DTOs;
using backend.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobEntriesController : ControllerBase
{
    private readonly JobTrackerContext _context;

    public JobEntriesController(JobTrackerContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobEntryResponseDto>>> GetJobEntriesAsync()
    {
        var userId = ParseUserId(User);
        var fetchedJobEntries =
            await _context.JobEntries
                .Where(jobEntry => jobEntry.UserId == userId)
                .Select(jobEntry => CreateJobEntryResponseDto(jobEntry))
                .ToListAsync();
        
        return Ok(fetchedJobEntries);
    }

    [HttpGet("{id}")]
    [ActionName("GetJobEntryByIdAsync")]
    public async Task<ActionResult<JobEntryResponseDto>> GetJobEntryByIdAsync(Guid id)
    {
        var jobEntry = await _context.JobEntries.FindAsync(id);
        var userId = ParseUserId(User);
        if (jobEntry is null || jobEntry.UserId != userId)
        {
            return NotFound();
        }
        
        return Ok(CreateJobEntryResponseDto(jobEntry));
    }

    [HttpPost]
    public async Task<ActionResult<JobEntryResponseDto>> CreateJobEntryAsync(CreateJobEntryDto jobEntry)
    {
        var newJobEntry = new JobEntry
        {
            UserId = ParseUserId(User),
            CompanyName = jobEntry.CompanyName,
            JobTitle = jobEntry.JobTitle,
            DateApplied = jobEntry.DateApplied,
            ApplicationStatus = jobEntry.ApplicationStatus,
            Notes = jobEntry.Notes,
            JobSource = jobEntry.JobSource,
            PostingUrl = jobEntry.PostingUrl,
            SalaryMax = jobEntry.SalaryMax,
            SalaryMin = jobEntry.SalaryMin,
            RecruiterName = jobEntry.RecruiterName,
            RecruiterEmail = jobEntry.RecruiterEmail,
            InterviewDate = jobEntry.InterviewDate,
        };

        _context.Add(newJobEntry);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetJobEntryByIdAsync), new { id = newJobEntry.JobEntryId }, CreateJobEntryResponseDto(newJobEntry)); 
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobEntryAsync(Guid id)
    {
        var jobEntryToRemove =  await _context.JobEntries.FindAsync(id);
        var userId =  ParseUserId(User);
        if (jobEntryToRemove is null || jobEntryToRemove.UserId != userId)
        {
            return NotFound();
        }
        _context.JobEntries.Remove(jobEntryToRemove);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobEntryAsync(Guid id, UpdateJobEntryDto jobEntry)
    {
        var jobEntryToUpdate = await _context.JobEntries.FindAsync(id);
        var userId = ParseUserId(User);
        if (jobEntryToUpdate is null || jobEntryToUpdate.UserId != userId) return NotFound();
        
        _context.Entry(jobEntryToUpdate).CurrentValues.SetValues(jobEntry);
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static Guid ParseUserId(ClaimsPrincipal claimsPrincipal)
    {
        return  Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Claim principal is missing."));
    }

    private static JobEntryResponseDto CreateJobEntryResponseDto(JobEntry jobEntry)
    {
        return new JobEntryResponseDto
        {
            JobEntryId = jobEntry.JobEntryId,
            CompanyName = jobEntry.CompanyName,
            JobTitle = jobEntry.JobTitle,
            DateApplied = jobEntry.DateApplied,
            ApplicationStatus = jobEntry.ApplicationStatus,
            Notes = jobEntry.Notes,
            JobSource = jobEntry.JobSource,
            PostingUrl = jobEntry.PostingUrl,
            SalaryMin = jobEntry.SalaryMin,
            SalaryMax = jobEntry.SalaryMax,
            RecruiterName = jobEntry.RecruiterName,
            RecruiterEmail = jobEntry.RecruiterEmail,
            InterviewDate = jobEntry.InterviewDate
        };
    } 
}
