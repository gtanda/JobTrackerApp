using backend.Models.DTOs;
using backend.Models.Enums;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IWebHostEnvironment _env;

    public AuthController(IAuthService authService, IWebHostEnvironment env)
    {
        _authService = authService;
        _env = env;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto dto)
    {
        var registerUserResponse = await _authService.RegisterAsync(dto);
        if (registerUserResponse.Succeeded)
        {
            SetRefreshCookie(registerUserResponse.Tokens!.RawRefreshToken);
            return Created(String.Empty, new AuthResponseDto
            {
                AccessToken = registerUserResponse.Tokens.AccessToken,
                TokenExpiration = registerUserResponse.Tokens.Expires,
            });
        }
        return registerUserResponse.Error switch
        {
            ErrorType.EmailAlreadyExists => Conflict("Email is already registered to a user."),
            _ => StatusCode(500, "Internal server error")
        };
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserDto dto)
    {
        var loginUserResponse = await _authService.LoginAsync(dto);
        if (loginUserResponse.Succeeded)
        {
            SetRefreshCookie(loginUserResponse.Tokens!.RawRefreshToken);
            return Ok(new AuthResponseDto
            {
                AccessToken = loginUserResponse.Tokens.AccessToken,
                TokenExpiration = loginUserResponse.Tokens.Expires,
            });
        }
        return loginUserResponse.Error switch
        {
            ErrorType.InvalidCredentials => Unauthorized("Invalid credentials."),
            _ => StatusCode(500, "Internal server error")
        };
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken is null) return Unauthorized("Missing refresh token");
        var refreshTokenResponse = await _authService.RefreshTokenAsync(refreshToken);
        if (refreshTokenResponse.Succeeded)
        {
            SetRefreshCookie(refreshTokenResponse.Tokens!.RawRefreshToken);
            return Ok(new AuthResponseDto
            {
                AccessToken = refreshTokenResponse.Tokens.AccessToken,
                TokenExpiration = refreshTokenResponse.Tokens.Expires,
            });
        };
        return refreshTokenResponse.Error switch
        {
            ErrorType.InvalidRefreshToken => Unauthorized("Invalid Refresh Token"),
            _ => StatusCode(500, "Internal server error")
        };
    }

    private void SetRefreshCookie(string rawToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = !_env.IsDevelopment(),
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", rawToken, cookieOptions);
    }

}