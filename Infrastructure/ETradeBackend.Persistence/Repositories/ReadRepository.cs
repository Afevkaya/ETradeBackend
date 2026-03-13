using System.Linq.Expressions;
using ETradeBackend.Application.Repositories;
using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Repositories;

public class ReadRepository<T>(ETradeDbContext eTradeDbContext) : IReadRepository<T> where T : BaseEntity
{
    public DbSet<T> Table { get; } = eTradeDbContext.Set<T>();
    public IQueryable<T> GetAll() => Table.AsQueryable();
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate) => Table.Where(predicate);
    public async Task<T> GetSingleWhereAsync(Expression<Func<T, bool>> predicate) => await Table.SingleOrDefaultAsync(predicate);
    public async Task<T> GetByIdAsync(Guid id) => await Table.FindAsync(id);
}