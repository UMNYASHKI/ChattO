using Application.Helpers;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IRepository<TEntity> where TEntity : class
{
    Task<Result<bool>> AddItemAsync(TEntity entity);

    Task<Result<TEntity>> GetByIdAsync(Guid id);

    Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate, int? pageNum, int? count);

    Task<Result<bool>> DeleteItemAsync(Guid id);

    Task<Result<bool>> UpdateItemAsync(TEntity entity);

    Task<Result<bool>> IsUnique(Expression<Func<TEntity, bool>> filter);
}
