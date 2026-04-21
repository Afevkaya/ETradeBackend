using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.ResetPassword;

public record ResetPasswordCommandRequest(string Email) : IRequest<ResetPasswordCommandResponse>;