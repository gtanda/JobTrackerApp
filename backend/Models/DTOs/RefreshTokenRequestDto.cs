namespace backend.Models.DTOs;

public record RefreshTokenRequestDto
{
    public required string RefreshToken { get; init; }
}