using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Domain.Entities.Identities;

namespace ETradeBackend.Domain.Entities.Baskets;

public class Basket : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    public Order Order { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}