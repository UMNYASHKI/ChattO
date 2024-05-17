using Application.Helpers;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IRepository<TEntity> where TEntity : class
{
    Task<Result<bool>> AddItemAsync(TEntity entity);

    Task<Result<bool>> AddItemsAsync(IEnumerable<TEntity> entities);

    Task<Result<TEntity>> GetByIdAsync(Guid id);

    Task<Result<TCurrent>> GetByIdAsync<TCurrent>(Guid id) where TCurrent : class;

    Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, int? pageNum = null, int? count = null);

    Task<Result<bool>> DeleteItemAsync(Guid id);

    Task<Result<bool>> DeleteItemAsync<TCurrent>(Guid id) where TCurrent : class;

    Task<Result<bool>> UpdateItemAsync(TEntity entity);

    Task<Result<bool>> IsUnique(Expression<Func<TEntity, bool>> filter);
}
