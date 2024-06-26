﻿using Application.Helpers;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IRepository<TEntity> where TEntity : class
{
    Task<Result<bool>> AddItemAsync(TEntity entity);

    Task<Result<bool>> AddItemsAsync(IEnumerable<TEntity> entities);

    Task<Result<TEntity>> GetByIdAsync(Guid id);

    Task<Result<IEnumerable<TEntity>>> GetAllAsync(
         Expression<Func<TEntity, bool>>? filter = null,
          Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> orderBy = null,
          int? pageNum = null, int? count = null);

    Task<Result<bool>> DeleteItemAsync(Guid id);

    Task<Result<bool>> UpdateItemAsync(TEntity entity);

    Task<Result<bool>> IsUnique(Expression<Func<TEntity, bool>> filter);

    Task<Result<int>> GetTotalCountAsync(Expression<Func<TEntity, bool>>? filter = null);

    Task<Result<bool>> PartialUpdateAsync<T>(Guid id, T entity);

    Task<Result<bool>> DeleteItemsAsync(IEnumerable<TEntity> entities);
}
