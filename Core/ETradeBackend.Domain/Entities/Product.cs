using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Domain.Entities.Files;

namespace ETradeBackend.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductImageFile> ProductImageFiles { get; set; }
}