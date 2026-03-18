using ETradeBackend.Application.Repositories.ProductImageFiles;
using ETradeBackend.Domain.Entities.Files;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.ProductImageFiles;

public class ProductImageFileWriteRepository(ETradeDbContext eTradeDbContext) : WriteRepository<ProductImageFile>(eTradeDbContext), IProductImageFileWriteRepository
{
}

