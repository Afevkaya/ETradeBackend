using System.Linq.Expressions;
using ETradeBackend.Domain.Entities.Common;

namespace ETradeBackend.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T: BaseEntity
{
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
    Task<T> GetSingleWhereAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(Guid id);
}