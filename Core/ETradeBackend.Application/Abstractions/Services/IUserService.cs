using ETradeBackend.Application.DTOs.Users;
using ETradeBackend.Domain.Entities.Identities;

namespace ETradeBackend.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser user);
    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpiration, int addOnAccessTokenDate);
    Task UpdatePasswordAsync(Guid userId, string resetToken, string newPassword);
}