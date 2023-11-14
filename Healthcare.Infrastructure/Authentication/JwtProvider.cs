using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Healthcare.Application.Core.Abstractions.Authentication;
using Healthcare.Domain.Entities;
using Healthcare.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Healthcare.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }
    
    public string Create(Guid userId, string email, UserPermission userPermission)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = 
        {
            new Claim("userId", userId.ToString()),
            new Claim("email_address", email),
            new Claim("user_permission", ((int)userPermission).ToString())
        };

        var tokenExpirationTime = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeSeconds);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}