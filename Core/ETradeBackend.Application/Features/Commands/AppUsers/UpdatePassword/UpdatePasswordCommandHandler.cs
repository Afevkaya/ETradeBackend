using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.Exceptions;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.UpdatePassword;

public class UpdatePasswordCommandHandler(IUserService userService) : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.Password.Equals(request.PasswordConfirm))
            throw new PasswordChangeFailedException("Lutfen şifreleri birebir giriniz");
        await userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
        return new UpdatePasswordCommandResponse();
    }
}