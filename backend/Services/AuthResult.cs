using backend.Models.DTOs;
using backend.Models.Enums;

namespace backend.Services;

public record AuthResult
{
    public bool Succeeded { get; init; }
    public AuthTokens? Tokens { get; init; }
    public ErrorType Error { get; init; }
    
    private AuthResult() {}

    public static AuthResult Success(AuthTokens tokens)
    {
        return new (){ Succeeded = true, Tokens = tokens,  Error = ErrorType.None };
    }

    public static AuthResult Failure(ErrorType error)
    {
        return new (){Succeeded = false, Error = error};
    }
}
