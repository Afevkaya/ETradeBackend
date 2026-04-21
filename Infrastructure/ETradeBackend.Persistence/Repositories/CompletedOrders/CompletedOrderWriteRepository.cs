using ETradeBackend.Application.Repositories.CompletedOrders;
using ETradeBackend.Domain.Entities.CompletedOrders;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.CompletedOrders;

public class CompletedOrderWriteRepository(ETradeDbContext eTradeDbContext) : WriteRepository<CompletedOrder>(eTradeDbContext), ICompletedOrderWriteRepository
{
    
}