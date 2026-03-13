using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Products;

public class ProductWriteRepository(ETradeDbContext eTradeDbContext) : WriteRepository<Product>(eTradeDbContext), IProductWriteRepository
{
    
}