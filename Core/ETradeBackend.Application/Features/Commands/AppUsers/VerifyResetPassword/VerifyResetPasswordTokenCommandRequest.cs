using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.VerifyResetPassword;

public record VerifyResetPasswordTokenCommandRequest(Guid UserId, string Token) : IRequest<VerifyResetPasswordTokenCommandResponse>;
