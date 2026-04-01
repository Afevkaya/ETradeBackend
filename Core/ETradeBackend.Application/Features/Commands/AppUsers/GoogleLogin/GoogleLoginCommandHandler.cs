using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.GoogleLogin;

public class GoogleLoginCommandHandler(IAuthService authService) : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginWithGoogleAsync(request.IdToken);
        return new GoogleLoginCommandResponse(result);
    }
}