using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await authService.RefreshTokenLoginAsync(request.RefreshToken);
        return new RefreshTokenLoginCommandResponse(token);
    }
}