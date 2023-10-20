using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using TradeSwing.Application.Common.Interfaces.Authentication;
using TradeSwing.Application.Common.Services;
using TradeSwing.Domain.Entities;
using static Microsoft.IdentityModel.Tokens.SecurityAlgorithms;

namespace TradeSwing.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(UserEntity userEntity)
    {
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.GivenName, userEntity.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, userEntity.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id.ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, 
            audience: _jwtSettings.Audience, 
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes), 
            claims: claims,
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}