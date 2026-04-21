using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.UpdatePassword;

public record UpdatePasswordCommandRequest(Guid UserId, string ResetToken, string Password, string PasswordConfirm): IRequest<UpdatePasswordCommandResponse>;