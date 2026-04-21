using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.VerifyResetPassword;

public class VerifyResetPasswordTokenCommandHandler(
    IAuthService authService) : IRequestHandler<VerifyResetPasswordTokenCommandRequest, VerifyResetPasswordTokenCommandResponse>
{
    public async Task<VerifyResetPasswordTokenCommandResponse> Handle(VerifyResetPasswordTokenCommandRequest request, CancellationToken cancellationToken)
    {        
        var result = await authService.VerifyResetPasswordTokenAsync(request.UserId, request.Token);
        return new(true);
    }
}