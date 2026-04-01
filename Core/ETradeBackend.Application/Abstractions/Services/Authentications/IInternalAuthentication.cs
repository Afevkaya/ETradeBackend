using ETradeBackend.Application.DTOs;

namespace ETradeBackend.Application.Abstractions.Services.Authentications;

public interface IInternalAuthentication
{
    Task<Token> LoginAsync(string usernameOrEmail, string password);
}