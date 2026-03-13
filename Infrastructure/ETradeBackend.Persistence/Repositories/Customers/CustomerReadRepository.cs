using ETradeBackend.Application.Repositories.Customers;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Customers;

public class CustomerReadRepository(ETradeDbContext eTradeDbContext) : ReadRepository<Customer>(eTradeDbContext), ICustomerReadRepository
{
    
}