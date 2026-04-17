
using ETradeBackend.Domain.Entities.Products;

namespace ETradeBackend.Domain.Entities.Files;

public class ProductImageFile : File
{
    public ICollection<ProductProductImageFiles> ProductProductImageFiles { get; set; }
}