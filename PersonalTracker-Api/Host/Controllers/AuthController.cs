using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
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
        var token = await _authManager.Register(
            request.Email,
            request.Password);

        if (token == null)
            return BadRequest();

        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authManager.Login(
            request.Email,
            request.Password);

        if (token == null)
            return Unauthorized();

        return Ok(token);
    }
}