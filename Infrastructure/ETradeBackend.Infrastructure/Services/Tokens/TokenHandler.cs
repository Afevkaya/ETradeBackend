using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using ETradeBackend.Application.Abstractions.Tokens;
using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ETradeBackend.Infrastructure.Services.Tokens;

public class TokenHandler(IConfiguration configuration) : ITokenHandler
{
    public Token CreateAccessToken()
    {
        Token token = new();
        // SecurityKey'in simetrik olduğunu belirtiyoruz.
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!));
        
        // Şifrelenmiş kimliği oluşturuyoruz.
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        
        var now = DateTime.UtcNow;
        var expirationMinutes = configuration.GetValue<int>("Token:Expiration:Seconds");
        
        // Oluşturulacak token ayarlarını veriyoruz.
        token.Expiration = now.AddSeconds(expirationMinutes);
        JwtSecurityToken jwtSecurityToken = new(
            audience: configuration["Token:Audience"],
            issuer: configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: now,
            signingCredentials: signingCredentials
        );
        
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
        token.RefreshToken = CreateRefreshToken();
        
        return token;
    }

    public string CreateRefreshToken()
    {
        var number = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}