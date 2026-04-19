namespace ETradeBackend.Application.DTOs.Orders.Responses;

public record SingleOrderResponse(Guid Id, string Address, string Description, string OrderCode, object OrderItems, decimal BasketTotalPrice, DateTime CreatedDate);