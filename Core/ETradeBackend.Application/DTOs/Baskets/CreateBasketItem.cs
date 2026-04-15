namespace ETradeBackend.Application.DTOs.Baskets;

public class CreateBasketItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}