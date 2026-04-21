namespace ETradeBackend.Application.DTOs.Orders.Responses;

public record CompletedOrderResponse(string OrderCode, DateTime OrderDate, string NameSurname, string Email);