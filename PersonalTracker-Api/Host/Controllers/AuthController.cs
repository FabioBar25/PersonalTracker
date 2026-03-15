using Microsoft.AspNetCore.Mvc;
using PersonalTracker.Api.Managers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthManager _authManager;

    public AuthController(AuthManager authManager)
    {
        _authManager = authManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var token = await _authManager.RegisterAsync(
            request.Email,
            request.Password);

        if (token == null)
            return BadRequest();

        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authManager.LoginAsync(
            request.Email,
            request.Password);

        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }
}