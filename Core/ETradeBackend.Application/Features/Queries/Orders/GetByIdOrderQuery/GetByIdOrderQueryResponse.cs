namespace ETradeBackend.Application.Features.Queries.Orders.GetByIdOrderQuery;

public record GetByIdOrderQueryResponse(Guid Id, string Address, string Description, string OrderCode, object OrderItems, decimal BasketTotalPrice, DateTime CreatedDate, bool Completed);