using ETradeBackend.Domain.Entities.Common;

namespace ETradeBackend.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T: BaseEntity
{
    Task<T> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entities);
    bool Update(T entity);
    bool UpdateRange(IEnumerable<T> entities);
    bool Delete(T entity);
    bool Delete(Guid id);
    bool DeleteRange(IEnumerable<T> entities);
    Task<int> SaveChangesAsync();
}