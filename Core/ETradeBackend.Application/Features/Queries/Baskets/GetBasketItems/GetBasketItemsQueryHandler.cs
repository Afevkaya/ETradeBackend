using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Baskets.GetBasketItems;

public class GetBasketItemsQueryHandler(
    IBasketService basketService) : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
{
    public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var basketItems = await basketService.GetBasketItemsAsync();
        return basketItems.Select(basketItem => new GetBasketItemsQueryResponse(basketItem.Id, basketItem.Product.Name,
            basketItem.Product.Price, basketItem.Quantity)).ToList();
    }
}