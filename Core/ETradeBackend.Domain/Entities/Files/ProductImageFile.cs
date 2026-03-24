
namespace ETradeBackend.Domain.Entities.Files;

public class ProductImageFile : File
{
    public ICollection<Product> Products { get; set; }
}