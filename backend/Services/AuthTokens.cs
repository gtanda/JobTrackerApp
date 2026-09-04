namespace backend.Services;

public record AuthTokens
{
    public required string AccessToken { get; init; }
    public required string RawRefreshToken { get; init; }
    public required DateTime Expires { get; init; }
}