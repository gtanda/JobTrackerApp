using backend.Models.Entities;

namespace backend.Services;

public interface ITokenService
{
    public AccessTokenResult GenerateJwtToken(User user);
    public RefreshTokenResult GenerateRefreshToken();
}