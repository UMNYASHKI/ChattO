using Application.Abstractions;
using Application.Helpers;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Services.DataAccess;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IChattoDbContext _context;

    private readonly DbSet<TEntity> _dbSet;

    public Repository(IChattoDbContext chattoDbContext)
    {
        _context = chattoDbContext;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<Result<bool>> AddItemAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return Result.Success(true);
        }
        catch
        {
            return Result.Failure<bool>($"Cannot add {typeof(TEntity).Name}");
        }
    }

    public async Task<Result<bool>> AddItemsAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return Result.Success(true);
        }
        catch
        {
            return Result.Failure<bool>($"Cannot add {typeof(TEntity).Name}s");
        }
    }

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
         Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> orderBy = null,
         int ? pageNum = null, int? count = null)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (pageNum != null && count != null)
            {
                query = query.Skip((int)((pageNum - 1) * count)).Take((int)count);
            }
            else if (count != null)
            {
                query = query.Take((int)count);
            }

            if (orderBy != null)
            {
                  return Result.Success<IEnumerable<TEntity>>(await orderBy.Compile()(query).ToListAsync());
            }
            else
            {
                return Result.Success<IEnumerable<TEntity>>(await query.ToListAsync());
            }
           
        }
        catch
        {
            return Result.Failure<IEnumerable<TEntity>>($"Cannot get {typeof(TEntity).Name}s");
        }

    }

    public async Task<Result<TEntity>> GetByIdAsync(Guid id)
    {
        try
        {
            var result = await _dbSet.FindAsync(id);

            if (result is null)
            {
                return Result.Failure<TEntity>($"Cannot find {typeof(TEntity).Name}");
            }

            return Result.Success(result);
        }
        catch
        {
            return Result.Failure<TEntity>($"Error when finding {typeof(TEntity).Name}");
        }
    }

    public async Task<Result<TCurrent>> GetByIdAsync<TCurrent>(Guid id) where TCurrent : class
    {
        try
        {
            var inheritedDbSet = _context.Set<TCurrent>();
            var result = await inheritedDbSet.FindAsync(id);

            if (result is null)
            {
                return Result.Failure<TCurrent>($"Cannot find {typeof(TCurrent).Name}");
            }

            return Result.Success(result);
        }
        catch
        {
            return Result.Failure<TCurrent>($"Error when finding {typeof(TCurrent).Name}");
        }
    }

    public async Task<Result<bool>> UpdateItemAsync(TEntity entityToUpdate)
    {
        try
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }
            _dbSet.Update(entityToUpdate);

            await _context.SaveChangesAsync();

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>($"Cannot update {typeof(TEntity).Name}");
        }
    }

    public async Task<Result<bool>> DeleteItemAsync(Guid id)
    {
        try
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);

            await _context.SaveChangesAsync();

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>($"Cannot delete the {typeof(TEntity).Name}");
        }
    }

    public async Task<Result<bool>> DeleteItemAsync<TCurrent>(Guid id) where TCurrent : class
    {
        try
        {
            var currentDbSet = _context.Set<TCurrent>();

            var entityToDelete = await currentDbSet.FindAsync(id);

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                currentDbSet.Attach(entityToDelete);
            }
            currentDbSet.Remove(entityToDelete);

            await _context.SaveChangesAsync();

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>($"Cannot delete the {typeof(TCurrent).Name}");
        }
    }

    public async Task<Result<bool>> IsUnique(Expression<Func<TEntity, bool>> filter)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            return Result.Success(!query.Any(filter));
        }
        catch
        {
            return Result.Failure<bool>($"Cannot check uniqueness of {typeof(TEntity).Name}");
        }
    }

    public async Task<Result<int>> GetTotalCountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return Result.Success(await query.CountAsync());
        }
        catch
        {
            return Result.Failure<int>($"Cannot get total count of {typeof(TEntity).Name}");
        }
    }
}
