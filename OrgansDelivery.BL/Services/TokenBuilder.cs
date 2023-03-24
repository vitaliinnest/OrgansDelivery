using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganStorage.BL.Models.Options;
using OrganStorage.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OrganStorage.BL.Services;

public interface ITokenBuilder
{
    Task<string> GenerateJwtTokenAsync(User user, Tenant tenant);
}

public class TokenBuilder : ITokenBuilder
{
    private readonly JwtSettings _jwtSettings;
    private readonly IClaimsService _claimsService;

    public TokenBuilder(
        IOptions<JwtSettings> jwtSettings,
        IClaimsService claimsCalculator
        )
    {
        _jwtSettings = jwtSettings.Value;
        _claimsService = claimsCalculator;
    }

    public async Task<string> GenerateJwtTokenAsync(User user, Tenant tenant)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = await _claimsService.GetClaimsForAuthUserAsync(user, tenant);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            claims: claims,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
