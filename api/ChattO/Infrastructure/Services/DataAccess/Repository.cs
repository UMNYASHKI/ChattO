using Application.Abstractions;
using Application.Helpers;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Result<bool>> AddItemAsync(TEntity entity)
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

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = null, int? pageNum = null, int? count = null)
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
                query = query.Skip((int)(pageNum * count)).Take((int)count);
            }
            else if (count != null)
            {
                query = query.Take((int)count);
            }

            return Result.Success<IEnumerable<TEntity>>(await query.ToListAsync());
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

    public async Task<Result<bool>> UpdateItemAsync(TEntity entityToUpdate)
    {
        try
        {
            CheckEntityEntryState(entityToUpdate);
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
            CheckEntityEntryState(entityToDelete);
            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();

            return Result.Success<bool>();
        }
        catch
        {
            return Result.Failure<bool>($"Cannot delete the {typeof(TEntity).Name}");
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

    private void CheckEntityEntryState(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
    }
}
