using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Roles.GetRoles;

public class GetRolesQueryHandler(IRoleService roleService) : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
{
    public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await roleService.GetAllAsync(request.Page, request.PageSize);
        return new GetRolesQueryResponse(result.datas, result.totalCount);
    }
}