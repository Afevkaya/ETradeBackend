using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Roles.DeleteRole;

public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
{
    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await roleService.DeleteAsync(request.Id);
        return new DeleteRoleCommandResponse(result);
    }
}