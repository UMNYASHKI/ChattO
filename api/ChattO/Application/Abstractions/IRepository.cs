using Application.Helpers;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<Result<bool>> AddItemAsync(TEntity entity);

    public Task<Result<TEntity>> GetByIdAsync(Guid id);

    public Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate, int? pageNum, int? count);

    public Task<Result<bool>> DeleteItemAsync(Guid id);

    public Task<Result<bool>> UpdateItemAsync(TEntity entity);
}
