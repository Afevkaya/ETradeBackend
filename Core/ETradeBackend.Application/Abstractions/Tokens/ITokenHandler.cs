using ETradeBackend.Application.DTOs;

namespace ETradeBackend.Application.Abstractions.Tokens;

public interface ITokenHandler
{
    Token CreateAccessToken();
}