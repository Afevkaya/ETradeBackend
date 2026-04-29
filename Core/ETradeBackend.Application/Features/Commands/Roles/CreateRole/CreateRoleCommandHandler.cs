using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.Roles.CreateRole;

public class CreateRoleCommandHandler(IRoleService roleService) : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await roleService.CreateAsync(request.Name);
        return new CreateRoleCommandResponse(result);
    }
    
}