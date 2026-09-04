using backend.Models.DTOs;
using backend.Models.Enums;

namespace backend.Services;

public record AuthResult
{
    public bool Succeeded { get; init; }
    public AuthResponseDto? Response { get; init; }
    public ErrorType Error { get; init; }
    
    private AuthResult() {}

    public static AuthResult Success(AuthResponseDto response)
    {
        return new (){ Succeeded = true, Response = response,  Error = ErrorType.None };
    }

    public static AuthResult Failure(ErrorType error)
    {
        return new (){Succeeded = false, Error = error};
    }



}
