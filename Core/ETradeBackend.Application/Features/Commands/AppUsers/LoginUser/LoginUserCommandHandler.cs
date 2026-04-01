using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.LoginUser;

public class LoginUserCommandHandler(IAuthService authService) : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
       var result = await authService.LoginAsync(request.UsernameOrEmail, request.Password);
       return new LoginUserCommandResponse(result);
    }
}