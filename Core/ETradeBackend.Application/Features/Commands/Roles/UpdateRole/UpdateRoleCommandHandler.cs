using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Roles.UpdateRole;

public class UpdateRoleCommandHandler(IRoleService roleService) : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
{
    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await roleService.UpdateAsync(request.Id, request.Name);
        return new UpdateRoleCommandResponse(result);
    }
}