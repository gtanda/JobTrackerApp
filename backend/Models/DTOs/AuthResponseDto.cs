namespace backend.Models.DTOs;

public record AuthResponseDto
{
    public required string AccessToken { get; init; }
    public required DateTime TokenExpiration { get; init; }
}