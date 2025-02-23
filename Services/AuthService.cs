using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiGodoy.Entities.SessionHistory.SessionHistoryDto;
using ApiGodoy.Entities.User.UserDto;
using ApiGodoy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{

    private readonly IConfiguration _config;
    private readonly UserService _userService;
    private readonly SessionHistoryService _sessionHistoryService;
    private readonly PasswordHasher<object> _hasher;

    public AuthService(IConfiguration config, UserService userService, SessionHistoryService sessionHistoryService)
    {
        _config = config;
        _userService = userService;
        _sessionHistoryService = sessionHistoryService;
        _hasher = new PasswordHasher<object>();
    }


    public async Task<Object> Authenticate(AuthUserDto UserDto, string audience)
    {
        var user = await _userService.GetByEmail(UserDto.Email);
        if (user == null) return null;
        if( user.Password != null)
        {

            var keys = _config["JwtSettings:Secret"];
            var PasswordVerification = _hasher.VerifyHashedPassword(null, user.Password, UserDto.Password);
            if (PasswordVerification != PasswordVerificationResult.Success) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]);
            var audiences = _config.GetSection("JwtSettings:Audience").Get<string[]>();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, user.Email),

            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = audience

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var sessionDto = new CreateSessionHistoryDto { UserId = user.Id };
            var session = await _sessionHistoryService.Add(sessionDto);
            
;            return new { token = tokenHandler.WriteToken(token), user = user, lastLogin = session};
        }

        return null;
    }
}
