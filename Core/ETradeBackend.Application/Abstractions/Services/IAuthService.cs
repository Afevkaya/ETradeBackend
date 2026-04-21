using ETradeBackend.Application.Abstractions.Services.Authentications;

namespace ETradeBackend.Application.Abstractions.Services;

public interface IAuthService : IInternalAuthentication, IExternalAuthentication
{
    Task ResetPasswordAsync(string email);
    Task<bool> VerifyResetPasswordTokenAsync(Guid userId, string resetToken);
}