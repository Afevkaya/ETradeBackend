using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;

namespace ETradeBackend.Application.Abstractions.Services.Authentications;

public interface IExternalAuthentication
{
    Task<Token> LoginWithGoogleAsync(string idToken);
}