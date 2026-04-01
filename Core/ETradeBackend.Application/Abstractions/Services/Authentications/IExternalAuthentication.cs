using ETradeBackend.Application.DTOs;

namespace ETradeBackend.Application.Abstractions.Services.Authentications;

public interface IExternalAuthentication
{
    Task<Token> LoginWithGoogleAsync(string idToken);
}