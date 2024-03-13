using API.Helpers;
using API.Services.Abstractions;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value;
    }

    public string GenerateJwtToken(AppUser user)
    {
        if (user is null)
        {
            throw new ArgumentNullException("User is not valid");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("SecurityStamp", user.SecurityStamp)
            };

        var expires = DateTime.UtcNow.AddDays(_jwtSettings.TokenValidityFromDays);

        var secToken = new JwtSecurityToken(_jwtSettings.Issuer,
          _jwtSettings.Audience,
          claims,
          expires: expires,
          signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(secToken);

        return token;
    }
}
