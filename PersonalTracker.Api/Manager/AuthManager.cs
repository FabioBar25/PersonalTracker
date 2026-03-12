using Microsoft.AspNetCore.Identity;

public class AuthManager
{
    private readonly JwtService _jwt;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthManager(
        UserManager<IdentityUser> userManager,
        JwtService jwt)
    {
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<string?> Register(string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return null;

        return _jwt.GenerateToken(user);
    }

    public async Task<string?> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        var valid = await _userManager.CheckPasswordAsync(user, password);

        if (!valid)
            return null;

        return _jwt.GenerateToken(user);
    }
}