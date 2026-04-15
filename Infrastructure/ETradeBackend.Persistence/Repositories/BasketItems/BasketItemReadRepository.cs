using ETradeBackend.Application.Repositories.BasketItems;
using ETradeBackend.Domain.Entities.Baskets;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.BasketItems;

public class BasketItemReadRepository(ETradeDbContext eTradeDbContext) : ReadRepository<BasketItem>(eTradeDbContext), IBasketItemReadRepository
{
    
}