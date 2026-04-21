using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.ResetPassword;

public class ResetPasswordCommandHandler(IAuthService authService) : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
{
    public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        await authService.ResetPasswordAsync(request.Email);
        return new ResetPasswordCommandResponse();
    }   
}