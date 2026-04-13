using ETradeBackend.Domain.Entities.Files;

namespace ETradeBackend.Domain.Entities;

public class ProductProductImageFiles
{
    public Guid ProductId { get; set; }
    public Guid ProductImageFileId { get; set; }
    public Product Product { get; set; }
    public ProductImageFile ProductImageFile { get; set; }
    public bool IsShowcase { get; set; }
}