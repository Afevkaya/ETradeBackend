using System.Linq.Expressions;
using ETradeBackend.Domain.Entities.Common;

namespace ETradeBackend.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T: BaseEntity
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true);
    Task<T> GetSingleWhereAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
    Task<T> GetByIdAsync(Guid id, bool tracking = true);
}