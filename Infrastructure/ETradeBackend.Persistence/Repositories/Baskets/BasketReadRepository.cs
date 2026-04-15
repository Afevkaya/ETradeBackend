using ETradeBackend.Application.Repositories.Baskets;
using ETradeBackend.Domain.Entities.Baskets;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Baskets;

public class BasketReadRepository(ETradeDbContext eTradeDbContext) : ReadRepository<Basket>(eTradeDbContext), IBasketReadRepository
{
    
}