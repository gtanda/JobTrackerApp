namespace backend.Services;

public record AccessTokenResult
{
    public required string AccessToken { get; init; }
    public required DateTime TokenExpiration { get; init; }
};