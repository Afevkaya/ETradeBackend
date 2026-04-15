namespace ETradeBackend.Application.DTOs.Baskets;

public class UpdateBasketItem
{
    public Guid BasketItemId { get; set; }
    public int Quantity { get; set; }
}