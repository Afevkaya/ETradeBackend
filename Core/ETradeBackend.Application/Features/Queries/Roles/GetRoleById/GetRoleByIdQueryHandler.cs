using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Roles.GetRoleById;

public class GetRoleByIdQueryHandler(IRoleService roleService) : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
{
    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await roleService.GetByIdAsync(request.Id);
        return new GetRoleByIdQueryResponse(result?.Id ?? Guid.Empty,
            result.HasValue ? result.Value.Name : string.Empty);
    }
}