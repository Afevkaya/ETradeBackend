using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;
using ETradeBackend.Domain.Entities.Identities;

namespace ETradeBackend.Application.Abstractions.Tokens;

public interface ITokenHandler
{
    Token CreateAccessToken(AppUser user);
    string CreateRefreshToken();
    
}