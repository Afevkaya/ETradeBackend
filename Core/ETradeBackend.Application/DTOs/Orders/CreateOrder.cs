namespace ETradeBackend.Application.DTOs.Orders;

public record CreateOrder(string Address, string Description, Guid BasketId);