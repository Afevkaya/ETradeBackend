using MediatR;

namespace ETradeBackend.Application.Features.Queries.Roles.GetRoleById;

public record GetRoleByIdQueryRequest(Guid Id) : IRequest<GetRoleByIdQueryResponse>;