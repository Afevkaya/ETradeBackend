using ETradeBackend.Application.Abstractions.Tokens;
using ETradeBackend.Domain.Entities.Identities;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Application.Features.Commands.AppUsers.GoogleLogin;

public class GoogleLoginCommandHandler(UserManager<AppUser> userManager,ITokenHandler tokenHandler) : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience =
            [
                "856596772318-l2kvd40sufhi1okjhh0t7erh25hd924m.apps.googleusercontent.com"
            ] // Google API Console'dan aldığınız Client ID
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, validationSettings);
        var userLoginInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
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
        
        var token = tokenHandler.CreateAccessToken();
        return new GoogleLoginCommandResponse(token);
    }
}