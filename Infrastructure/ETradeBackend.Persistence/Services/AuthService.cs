using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.Abstractions.Tokens;
using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Application.Helpers;
using ETradeBackend.Domain.Entities.Identities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETradeBackend.Persistence.Services;

public class AuthService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IUserService userService,
    ITokenHandler tokenHandler,
    IMailService mailService,
    IConfiguration configuration) : IAuthService
{
    public async Task<Token> LoginAsync(string usernameOrEmail, string password)
    {
        // Authentication
        var user = await userManager.FindByNameAsync(usernameOrEmail) ?? await userManager.FindByEmailAsync(usernameOrEmail);
        if (user == null) throw new UserNotFoundException();
        var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

        // Authorization
        if (!result.Succeeded) throw new AuthenticationErrorException();
        var token = tokenHandler.CreateAccessToken(user);
        await userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 60);
        return token;
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user == null || user.RefreshTokenExpiration <= DateTime.UtcNow) throw new UserNotFoundException();
        var token = tokenHandler.CreateAccessToken(user);
        await userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 60);
        return token;
    }

    public async Task<Token> LoginWithGoogleAsync(string idToken)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience =
            [
                configuration["ExternalLogins:Google:ClientId"]
            ] // Google API Console'dan aldığınız Client ID
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);
        var userLoginInfo = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        var user = await userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
        var result = user != null;
        if(user == null)
        {
            user = await userManager.FindByEmailAsync(payload.Email);
            if(user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = payload.Email,
                    Email = payload.Email,
                    NameSurname = payload.Name
                };
                // aspnetusers tablosuna kaydet
                var identityResult = await userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        // aspnetuserlogins tablosuna kaydet
        if (result)
            await userManager.AddLoginAsync(user, userLoginInfo);
        else
            throw new Exception("Google ile giriş başarısız oldu.");
        
        var token = tokenHandler.CreateAccessToken(user);
        await userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
        return token;
    }

    public async Task ResetPasswordAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) throw new UserNotFoundException();
        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        // var tokenBytes = System.Text.Encoding.UTF8.GetBytes(resetToken);
        // resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
        resetToken = resetToken.UrlEncode();
        await mailService.SendResetPasswordMailAsync(email, user.Id, resetToken);
    }

    public async Task<bool> VerifyResetPasswordTokenAsync(Guid userId, string resetToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        // var tokenBytes = WebEncoders.Base64UrlDecode(token);
        // var decodedToken = System.Text.Encoding.UTF8.GetString(tokenBytes);
        resetToken = resetToken.UrlDecode();
        await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        return true;
    }
}