using ETradeBackend.Application.Abstractions.Tokens;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Domain.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Application.Features.Commands.AppUsers.LoginUser;

public class LoginUserCommandHandler(
    UserManager<AppUser> userManager, 
    SignInManager<AppUser> signInManager,
    ITokenHandler tokenHandler) : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        // Authentication
        var user = await userManager.FindByNameAsync(request.UsernameOrEmail) ?? await userManager.FindByEmailAsync(request.UsernameOrEmail);
        if (user == null) throw new NotFoundUserException();
        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        // Authorization
        if (!result.Succeeded) throw new AuthenticationErrorException();
        var token = tokenHandler.CreateAccessToken();
        return new LoginUserCommandResponse(token);
    }
}