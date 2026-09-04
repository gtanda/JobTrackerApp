using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class TokenService : ITokenService
{
    private readonly string _jwtKey;
    private readonly string _jwtAudience;
    private readonly string _jwtIssuer;
    public TokenService(IConfiguration configuration)
    {
        _jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Missing JWT Key");
        _jwtAudience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Missing JWT Audience");
        _jwtIssuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Missing JWT Issuer");
    }

    public AccessTokenResult GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())}),
            Audience = _jwtAudience,
            Issuer = _jwtIssuer,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var  tokenString = tokenHandler.WriteToken(token);
        var accessTokenResult = new AccessTokenResult {
            AccessToken = tokenString, 
            TokenExpiration = tokenDescriptor.Expires.Value
            
        };
        return accessTokenResult;
    }

    public RefreshTokenResult GenerateRefreshToken()
    {
        var encodedToken = Convert.ToBase64String( RandomNumberGenerator.GetBytes(32));
        var hashedToken = SHA256.HashData(Encoding.UTF8.GetBytes(encodedToken));
        var refreshTokenResult = new RefreshTokenResult
        {
            RawToken = encodedToken,
            HashedToken = Convert.ToBase64String(hashedToken),
            Expires = DateTime.UtcNow.AddDays(7)
        };
        return refreshTokenResult;
    }
}