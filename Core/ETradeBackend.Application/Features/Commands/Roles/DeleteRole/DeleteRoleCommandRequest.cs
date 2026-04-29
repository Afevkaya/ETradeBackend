using MediatR;

namespace ETradeBackend.Application.Features.Commands.Roles.DeleteRole;

public record DeleteRoleCommandRequest(Guid Id) : IRequest<DeleteRoleCommandResponse>;