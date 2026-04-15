using MediatR;

namespace ETradeBackend.Application.Features.Queries.Baskets.GetBasketItems;

public record GetBasketItemsQueryRequest() : IRequest<List<GetBasketItemsQueryResponse>>;