using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;

namespace ETradeBackend.Application.Abstractions.Services.Authentications;

public interface IInternalAuthentication
{
    Task<Token> LoginAsync(string usernameOrEmail, string password);
    Task<Token> RefreshTokenLoginAsync(string refreshToken);
}