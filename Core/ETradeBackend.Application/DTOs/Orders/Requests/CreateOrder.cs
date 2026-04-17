namespace ETradeBackend.Application.DTOs.Orders.Requests;

public record CreateOrder(string Address, string Description, Guid BasketId);