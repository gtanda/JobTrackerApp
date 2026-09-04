using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTOs;

public record RegisterUserDto
{
    [EmailAddress]
    [MaxLength(254)]
    public required string Email { get; init; }
    [StringLength(128, MinimumLength = 8)]
    public required string Password { get; init; }
    [MaxLength(50)]
    public string? Username { get; init; }
}