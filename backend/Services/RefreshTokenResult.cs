namespace backend.Services;

public record RefreshTokenResult
{
    public required string RawToken { get; init; }
    public required string HashedToken { get; init; }
    public required DateTime Expires { get; init; }
}