using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.DTOs.Users;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Application.Helpers;
using ETradeBackend.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Persistence.Services;

public class UserService(UserManager<AppUser> userManager) : IUserService
{
    public async Task<CreateUserResponse> CreateAsync(CreateUser user)
    {
        var result = await userManager.CreateAsync(new AppUser
        {
            Id = Guid.NewGuid(),
            NameSurname = user.NameSurname,
            UserName = user.Username,
            Email = user.Email
        }, user.Password);

        if (result.Succeeded)
            return new CreateUserResponse(true, "Kullanıcı başarıyla oluşturuldu.");
        
        var errors = string.Join(", ", result.Errors.Select(e => new
        {
            e.Description,
            e.Code
        }));
        return new CreateUserResponse(false, $"Kullanıcı oluşturulamadı: {errors}");
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpiration, int addOnAccessTokenDate)
    {
        if (user == null) throw new UserNotFoundException();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = accessTokenExpiration.AddSeconds(addOnAccessTokenDate);
        await userManager.UpdateAsync(user);
    }

    public async Task UpdatePasswordAsync(Guid userId, string resetToken, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new UserNotFoundException();
        resetToken = resetToken.UrlDecode();
        var result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);
        if (result.Succeeded) await userManager.UpdateSecurityStampAsync(user);
        else throw new PasswordChangeFailedException();

    }
}