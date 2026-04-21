using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Domain.Entities.Orders;

namespace ETradeBackend.Domain.Entities.CompletedOrders;

public class CompletedOrder : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}