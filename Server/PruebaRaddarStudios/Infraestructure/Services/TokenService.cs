using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.DTOs;

namespace PruebaRaddarStudios.Infraestructure.Services;

public class TokenService(IConfiguration _configuration) : ITokenService
{
    public string Generate(UserDataDto userData)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
            new Claim(ClaimTypes.Email, userData.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
