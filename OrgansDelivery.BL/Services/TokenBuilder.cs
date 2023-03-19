using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrgansDelivery.BL.Models.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OrgansDelivery.BL.Services;

public interface ITokenBuilder
{
    Task<string> GenerateJwtTokenAsync(Guid userId);
}

public class TokenBuilder : ITokenBuilder
{
    private readonly JwtSettings _jwtSettings;
    private readonly IClaimsCalculator _claimsCalculator;

    public TokenBuilder(
        IOptions<JwtSettings> jwtSettings
        )
    {
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> GenerateJwtTokenAsync(Guid userId)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = await _claimsCalculator.GetClaimsForAuthUserAsync(userId);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            claims: claims,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
