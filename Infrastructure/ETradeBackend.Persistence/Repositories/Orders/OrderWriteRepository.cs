using ETradeBackend.Application.Repositories.Orders;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Orders;

public class OrderWriteRepository(ETradeDbContext eTradeDbContext) : WriteRepository<Order>(eTradeDbContext), IOrderWriteRepository
{
    
}