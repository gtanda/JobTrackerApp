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

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto dto)
    {
        var registerUserResponse = await _authService.RegisterAsync(dto);
        if (registerUserResponse.Succeeded) return Created(String.Empty, registerUserResponse.Response);
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
        if (loginUserResponse.Succeeded) return Ok(loginUserResponse.Response);
        return loginUserResponse.Error switch
        {
            ErrorType.InvalidCredentials => Unauthorized("Invalid credentials."),
            _ => StatusCode(500, "Internal server error")
        };
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto dto)
    {
        var refreshTokenResponse = await _authService.RefreshTokenAsync(dto);
        if (refreshTokenResponse.Succeeded) return Ok(refreshTokenResponse.Response);
        return refreshTokenResponse.Error switch
        {
            ErrorType.InvalidRefreshToken => Unauthorized("Invalid Refresh Token"),
            _ => StatusCode(500, "Internal server error")
        };
    }

}