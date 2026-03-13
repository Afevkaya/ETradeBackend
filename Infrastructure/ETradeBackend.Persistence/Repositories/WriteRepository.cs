using ETradeBackend.Application.Repositories;
using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Repositories;

public class WriteRepository<T>(ETradeDbContext eTradeDbContext) : IWriteRepository<T> where T: BaseEntity
{
    public DbSet<T> Table { get; } = eTradeDbContext.Set<T>();
    public async Task<T> AddAsync(T entity)
    {
        var result =  await Table.AddAsync(entity);
        return result.Entity;
    }
    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }
    public bool Update(T entity)
    {
        Table.Update(entity);
        return true;
    }
    public bool UpdateRange(IEnumerable<T> entities)
    {
        Table.UpdateRange(entities);
        return true;
    }
    public bool Delete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity);
        return true;
    }
    public bool Delete(Guid id)
    {
        var entity = Table.Find(id);
        if (entity == null) return false;
        Delete(entity);
        return true;
    }
    public bool DeleteRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            entity.IsDeleted = true;
        
        UpdateRange(entities);
        return true;
    }
    public async Task<int> SaveChangesAsync() => await eTradeDbContext.SaveChangesAsync();
}