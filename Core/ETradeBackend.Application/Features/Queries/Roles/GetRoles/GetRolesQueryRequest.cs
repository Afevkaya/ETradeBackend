using MediatR;

namespace ETradeBackend.Application.Features.Queries.Roles.GetRoles;

public record GetRolesQueryRequest(int Page, int PageSize) : IRequest<GetRolesQueryResponse>;