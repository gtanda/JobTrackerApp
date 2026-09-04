using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTOs;

public record LoginUserDto
{
    [EmailAddress]
    [MaxLength(254)]
    public required string Email { get; init; }
    
    public required string Password { get; init; }
}