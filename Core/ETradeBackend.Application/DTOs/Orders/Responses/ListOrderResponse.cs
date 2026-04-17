namespace ETradeBackend.Application.DTOs.Orders.Responses;

public record ListOrderResponse(int TotalOrderCount, object Orders);