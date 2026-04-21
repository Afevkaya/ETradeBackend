using ETradeBackend.Domain.Entities.Baskets;
using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Domain.Entities.CompletedOrders;

namespace ETradeBackend.Domain.Entities.Orders;

public class Order : BaseEntity
{
    public string Description { get; set; }
    public string Address { get; set; }
    public Basket Basket { get; set; }

    public string Code { get; set; }
    public CompletedOrder CompletedOrder { get; set; }
}