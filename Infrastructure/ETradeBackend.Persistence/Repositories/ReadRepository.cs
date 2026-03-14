using System.Linq.Expressions;
using ETradeBackend.Application.Repositories;
using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Repositories;

public class ReadRepository<T>(ETradeDbContext eTradeDbContext) : IReadRepository<T> where T : BaseEntity
{
    public DbSet<T> Table { get; } = eTradeDbContext.Set<T>();

    private IQueryable<T> GetQueryable() => Table.AsQueryable().Where(x=>x.IsDeleted == false);
    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = GetQueryable();
        if (!tracking) query.AsNoTracking();
        return query;
    }
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true) 
    {
        var query = GetQueryable().Where(predicate);
       if (!tracking) query.AsNoTracking();
       return query;
    }
    public async Task<T> GetSingleWhereAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
    {
        var query = GetQueryable();
        if (!tracking) query.AsNoTracking();
        return await query.FirstOrDefaultAsync(predicate);
    }
    public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
    {
        var query = GetQueryable();
        if (!tracking) query.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}