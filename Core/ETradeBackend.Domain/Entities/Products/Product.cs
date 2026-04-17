using ETradeBackend.Domain.Entities.Baskets;
using ETradeBackend.Domain.Entities.Common;

namespace ETradeBackend.Domain.Entities.Products;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    // public ICollection<Order> Orders { get; set; }
    public ICollection<ProductProductImageFiles> ProductProductImageFiles { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}