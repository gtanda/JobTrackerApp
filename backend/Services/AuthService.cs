using backend.Data;
using backend.Models.DTOs;
using backend.Models.Entities;
using backend.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class AuthService : IAuthService
{
    private readonly JobTrackerContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;
    
    public AuthService(JobTrackerContext jobTrackerContext, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
    {
        _context = jobTrackerContext;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }
    
    public async Task<AuthResult> RegisterAsync(RegisterUserDto dto)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
        if (userExists is not null)
        {
            return AuthResult.Failure(ErrorType.EmailAlreadyExists);
        }

        var user = new User
        {
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
            Username = dto.Username
        };
        
        user.PasswordHash =  _passwordHasher.HashPassword(user, dto.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return AuthResult.Success(await IssueTokenAsync(user));
    }

    public async Task<AuthResult> LoginAsync(LoginUserDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
        if (user is null || user.PasswordHash is null)
        {
            return AuthResult.Failure(ErrorType.InvalidCredentials);
        }
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return AuthResult.Failure(ErrorType.InvalidCredentials);
        
        return AuthResult.Success(await IssueTokenAsync(user));
    }

    public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
    {
        var hashedToken = _tokenService.HashRefreshToken(refreshToken);
        var existingToken = await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(token => token.TokenHash == hashedToken);
        
        if (existingToken is null || existingToken.Expiration < DateTime.UtcNow) return AuthResult.Failure(ErrorType.InvalidRefreshToken);
        
        _context.RefreshTokens.Remove(existingToken);
        return AuthResult.Success(await IssueTokenAsync(existingToken.User!));
    }

    private async Task<AuthTokens> IssueTokenAsync(User user)
    {
        var tokenRes =  _tokenService.GenerateJwtToken(user);
        var refreshTokenRes = _tokenService.GenerateRefreshToken();
        var refreshToken = new RefreshToken
        {
            TokenHash = refreshTokenRes.HashedToken,
            Expiration = refreshTokenRes.Expires,
            User = user,
            UserId = user.UserId
        };
        _context.RefreshTokens.Add(refreshToken); 
        await _context.SaveChangesAsync();
        return new AuthTokens {AccessToken = tokenRes.AccessToken, RawRefreshToken= refreshTokenRes.RawToken, Expires = tokenRes.TokenExpiration};
    }
}