using MediatR;

namespace ETradeBackend.Application.Features.Commands.Roles.CreateRole;

public record CreateRoleCommandRequest(string Name) : IRequest<CreateRoleCommandResponse>;