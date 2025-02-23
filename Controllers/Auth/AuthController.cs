using ApiGodoy.Entities.User.UserDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly AuthService _authService;

    public AuthController(IConfiguration config, AuthService authService)
    {
        _config = config;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthUserDto userDto)

    {
        string audience = Request.Headers.Origin;
        var result = await _authService.Authenticate(userDto,audience);

        if (result == null)
        {
            return Unauthorized(new { message = "Credenciales incorrectas" });
        }

        var token = result.GetType().GetProperty("token")?.GetValue(result, null) as string;

        if (!string.IsNullOrEmpty(token))
        {
            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(2),
                Path = "/",
                Domain = "localhost",
            });
        }

        return Ok(new { message = "Autenticado correctamente" });
    }

    [HttpGet("validate-token")]
    public IActionResult ValidateToken()
    {
                
        var token = Request.Cookies["jwtToken"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "No autorizado" });
        }
        return Ok(new { message = "Token válido" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        if (Request.Cookies.ContainsKey("jwtToken"))
        {
            Response.Cookies.Delete("jwtToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }

        return Ok(new { message = "Logout exitoso" });
    }
}
