namespace ETradeBackend.Application.Features.Queries.Baskets.GetBasketItems;

public record GetBasketItemsQueryResponse(Guid BasketItemId, string ProductName, decimal Price, int Quantity);