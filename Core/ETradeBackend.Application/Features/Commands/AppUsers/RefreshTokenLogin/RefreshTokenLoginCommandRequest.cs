using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.RefreshTokenLogin;

public record RefreshTokenLoginCommandRequest(string RefreshToken) : IRequest<RefreshTokenLoginCommandResponse>;