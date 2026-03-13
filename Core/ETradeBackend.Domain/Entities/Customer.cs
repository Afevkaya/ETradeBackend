using ETradeBackend.Domain.Entities.Common;

namespace ETradeBackend.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Order> Orders { get; set; }
}