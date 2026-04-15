using ETradeBackend.Application.Repositories.BasketItems;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.BasketItems;

public class BasketItemWriteRepository(ETradeDbContext eTradeDbContext) 
    : WriteRepository<Domain.Entities.Baskets.BasketItem>(eTradeDbContext), IBasketItemWriteRepository
{
    
}