using MediatR;

namespace ETradeBackend.Application.Features.Commands.Roles.UpdateRole;

public record UpdateRoleCommandRequest(Guid Id, string Name) : IRequest<UpdateRoleCommandResponse>;