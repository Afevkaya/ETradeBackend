namespace ETradeBackend.Application.Features.Queries.Orders.GetAllOrder;

public record GetAllOrderQueryResponse(int TotalOrderCount, object Orders);