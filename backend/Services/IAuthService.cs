using backend.Models.DTOs;
namespace backend.Services;

public interface IAuthService
{
    public Task<AuthResult> RegisterAsync(RegisterUserDto dto);
    public Task<AuthResult> LoginAsync(LoginUserDto dto);
    public Task<AuthResult> RefreshTokenAsync(string rawRefreshToken);
}