using ETradeBackend.Application.Repositories.Baskets;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Baskets;

public class BasketWriteRepository(ETradeDbContext eTradeDbContext) : WriteRepository<Domain.Entities.Baskets.Basket>(eTradeDbContext), IBasketWriteRepository
{
    
}