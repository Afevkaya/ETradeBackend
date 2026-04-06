using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;

namespace ETradeBackend.Application.Abstractions.Tokens;

public interface ITokenHandler
{
    Token CreateAccessToken();
    string CreateRefreshToken();
    
}