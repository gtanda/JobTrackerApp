using backend.Data;
using backend.Models.DTOs;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;


[ApiController]
[Route("api/[controller]")]
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
        var fetchedJobEntries =
            await _context.JobEntries.Select(jobEntry => CreateJobEntryResponseDto(jobEntry)).ToListAsync();
        
        return Ok(fetchedJobEntries);
    }

    [HttpGet("{id}")]
    [ActionName("GetJobEntryByIdAsync")]
    public async Task<ActionResult<JobEntryResponseDto>> GetJobEntryByIdAsync(Guid id)
    {
        var jobEntry = await _context.JobEntries.FindAsync(id);

        if (jobEntry is null)
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
            UserId = Guid.Parse("01a05b30-2eb1-7cb3-a98e-201342296a9f"), // TEMP
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

        if (jobEntryToRemove is null)
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
        
        if (jobEntryToUpdate is null) return NotFound();
        
        _context.Entry(jobEntryToUpdate).CurrentValues.SetValues(jobEntry);
        
        await _context.SaveChangesAsync();
        return NoContent();
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
