using Account.Application;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Account.Infrastructure;

public class JwtProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? throw new NullReferenceException();
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var secret = GenerateHashSecret(_jwtSettings.Secret);
        Console.WriteLine(secret);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JsonWebTokenHandler();

        return tokenHandler.CreateToken(tokenDescriptor);
    }

    public async Task<string?> ValidateTokenAsync(string token)
    {
        var secret = GenerateHashSecret(_jwtSettings.Secret);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var tokenHandler = new JsonWebTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,

            ValidateLifetime = true, 
            ClockSkew = TimeSpan.Zero,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
        };

        var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);

        return result.ClaimsIdentity.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }

    private string GenerateHashSecret(string input)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hash);
    }
}
