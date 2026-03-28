using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.LoginUser;

public record LoginUserCommandRequest(string UsernameOrEmail, string Password) : IRequest<LoginUserCommandResponse>;