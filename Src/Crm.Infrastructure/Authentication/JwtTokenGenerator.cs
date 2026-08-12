using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Crm.Infrastructure.Authentication;

public class JwtTokenGenerator(
    IConfiguration configuration) : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var jwtSection = configuration.GetSection("Jwt");

        var secretKey = jwtSection["SecretKey"]
            ?? throw new InvalidOperationException(
                "JWT SecretKey is not configured.");

        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName)
        };

        if (user.IsSuperAdmin)
        {
            claims.Add(
                new Claim("IsSuperAdmin", "true"));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}